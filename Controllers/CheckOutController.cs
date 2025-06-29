using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Extension;
using ShopOnline.Models;
using ShopOnline.Models.Authentication;
using ShopOnline.ViewModel;

namespace ShopOnline.Controllers
{
    [Authentication]
    public class CheckOutController : Controller
    {
        private readonly WebBanhangDbContext _context;
        public CheckOutController(WebBanhangDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart");
            var TaiKhoanID = HttpContext.Session.GetInt32("CusTomerID");
            MuaHangVM model = new MuaHangVM();
            if (TaiKhoanID != null)
            {
                var customer = _context.Customers.Find(TaiKhoanID);
                if (customer != null)
                {
                    model.CustomerId = customer.CustomerId;
                    model.FullName = customer.FullName;
                    model.Email = customer.Email;
                    model.Phone = customer.Phone;
                    model.Address = customer.Address;

                }
            }
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "ShoppingCart"); // Chuyển về giỏ hàng nếu rỗng
            }

            ViewBag.GioHang = cart;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MuaHangVM muaHang)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart");
            if (cart == null || !cart.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống");
                return RedirectToAction("Index", "ShoppingCart");
            }

            int customerId = 0;
            var taikhoanid = HttpContext.Session.GetString("CustomerId");

            if (!string.IsNullOrEmpty(taikhoanid))
            {

                customerId = Convert.ToInt32(taikhoanid);
                var existingCustomer = _context.Customers.SingleOrDefault(x => x.CustomerId == customerId);

                if (existingCustomer != null)
                {

                    existingCustomer.FullName = muaHang.FullName;
                    existingCustomer.Phone = muaHang.Phone;
                    existingCustomer.Email = muaHang.Email;
                    existingCustomer.Address = muaHang.Address;
                    existingCustomer.LocationId = null;
                    _context.Update(existingCustomer);
                    _context.SaveChanges();
                }
            }
            else
            {

                var newCustomer = new Customer
                {
                    FullName = muaHang.FullName,
                    Phone = muaHang.Phone,
                    Email = muaHang.Email,
                    Address = muaHang.Address,
                    CreateDate = DateTime.Now,
                    Active = true
                };

                _context.Customers.Add(newCustomer);
                _context.SaveChanges();

                customerId = newCustomer.CustomerId;


                HttpContext.Session.SetString("CustomerId", customerId.ToString());
            }

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    ShipDate = DateTime.Now.AddDays(3),
                    Note = muaHang.Note,
                    PaymentId = muaHang.PaymentId,
                    TransactStatusId = 1,
                    Paid = false,
                    Deleted = false,
                    OrderDetails = new List<OrderDetail>()
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var item in cart)
                {
                    var detail = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Discount = item.discount,
                        Total = item.Price*item.Quantity,
                        OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 10),
                        OrderId = order.OrderId
                    };
                    _context.OrderDetails.Add(detail);
                }

                _context.SaveChanges();

                HttpContext.Session.Remove("Cart");
                return RedirectToAction("DatHangThanhCong");
            }

            ViewBag.GioHang = cart;
            return View(muaHang);
        }


        public IActionResult DatHangThanhCong()
        {
            return View();
        }
        public IActionResult DonHang()
        {
            var customerId = Convert.ToUInt32(HttpContext.Session.GetString("CustomerId"));
            if (customerId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var orders = _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.CustomerId == customerId && o.Deleted == false) // Explicitly compare nullable bool  
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            return View(orders);
        }
    }
    }

