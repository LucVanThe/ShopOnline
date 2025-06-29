
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.ViewModel;
using ShopOnline.Extension;

namespace ShopOnline.Controllers
{
    public class CartAjaxRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class ShoppingCartController : Controller
    {
        private readonly WebBanhangDbContext _context;
        public ShoppingCartController(WebBanhangDbContext context)
        {
            _context = context;
        }
        //public List<CartItemViewModel> GioHang
        //{
        //    get
        //    {
        //        var gh = HttpContext.Session.Get<List<CartItemViewModel>>("GioHang");
        //        if(gh==default(List<CartItemViewModel>))
        //        {
        //            gh = new List<CartItemViewModel>();
        //        }
        //        return gh;
        //    }
        //}
        //[HttpPost]
        //[Route("api/cart/add")]
        //public IActionResult Addt0Cart(int productid, int? quantity)
        //{
        //    List<CartItemViewModel> gh = GioHang;
        //    try
        //    {
        //        CartItemViewModel item = GioHang.SingleOrDefault(x => x.product.ProductId == productid);
        //        if (item != null)
        //        {
        //            if (quantity.HasValue)
        //            {
        //                item.quantity += quantity.Value;
        //            }
        //            else
        //            {
        //                item.quantity++;
        //            }
        //        }
        //        else
        //        {
        //            Product hh = _context.Products.SingleOrDefault(x => x.ProductId == productid);
        //            item = new CartItemViewModel
        //            {

        //                quantity = quantity.HasValue ? quantity.Value : 1,
        //                product = hh
        //            };
        //            gh.Add(item);
        //        }
        //        HttpContext.Session.Set<List<CartItemViewModel>>("GioHang", gh);
        //        return Json(new { success = true });
        //    }
        //    catch 
        //    {

        //        return Json(new { success = false });
        //    }
        //    //them san pham

        //}
        //[HttpPost]
        //public IActionResult AddToCart(int productId, int quantity)
        //{
        //    var product = _context.Products.Find(productId);
        //    if (product == null)
        //        return NotFound();

        //    var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

        //    var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);
        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += quantity;
        //    }
        //    else
        //    {
        //        cart.Add(new CartItemViewModel
        //        {
        //            ProductId = product.ProductId,
        //            ProductName = product.ProductName,
        //            ProductImage = product.Thumb,
        //            Quantity = quantity,
        //            Price = product.Price * (1 - (product.Discount / 100))
                  
                    
        //        });
        //    }

        //    HttpContext.Session.SetObject("Cart", cart);

        //    return RedirectToAction("Index", "Shop"); // hoặc quay lại trang chi tiết sản phẩm
        //}
        [HttpPost]
        public JsonResult AddToCartAjax([FromBody] CartAjaxRequest model)
        {
            var product = _context.Products.Find(model.ProductId);
            if (product == null) return Json(new { success = false, message = "Không tìm thấy sản phẩm." });

            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += model.Quantity;
            }
            else
            {
                cart.Add(new CartItemViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductImage = product.Thumb,
                    discount=product.Discount,
                    Quantity = model.Quantity,
                    Price = product.Price * (1 - (product.Discount / 100))
                });
            }

            HttpContext.Session.SetObject("Cart", cart);

            return Json(new { success = true, message = "Đã thêm vào giỏ hàng!" });
        }
        [HttpPost]
        public JsonResult UpdateCartAjax([FromBody] List<CartItemViewModel> model)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart");
            if (cart == null || !cart.Any())
            {
                return Json(new { success = false, message = "Giỏ hàng trống." });
            }
            foreach (var update in model)
            {
                var item = cart.FirstOrDefault(x => x.ProductId == update.ProductId);
                if(item != null)
                {
                    if (update.Quantity <= 0)
                    {
                        cart.Remove(item); // Xóa sản phẩm nếu số lượng cập nhật là 0 hoặc âm
                    }
                    else
                    {
                        item.Quantity = update.Quantity; // Cập nhật số lượng sản phẩm
                    }
                }
            }
            HttpContext.Session.SetObject("Cart", cart);
            return Json(new { success = true, message = "Giỏ hàng đã được cập nhật!" });
        }
        [HttpPost]
        public JsonResult RemoveFromCartAjax(int id)
        {
            var cart=HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            var item = cart.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
            }
                HttpContext.Session.SetObject("Cart", cart);
                return Json(new { success = true, message = "Sản phẩm đã được xóa khỏi giỏ hàng." });
            
        }


        //[HttpPost]
        //[Route("api/cart/remove")]
        //public IActionResult Remove(int productid)
        //{
        //    try
        //    {
        //        List<CartItemViewModel> gh = GioHang;
        //        CartItemViewModel item = gh.SingleOrDefault(x => x.product.ProductId == productid);
        //        if(item != null)
        //        {
        //            gh.Remove(item);

        //        }
        //        HttpContext.Session.Set<List<CartItemViewModel>>("GioHang", gh);
        //        return Json(new { success = true });
        //    }
        //    catch 
        //    {

        //        return Json(new { success = false });
        //    }

        //}
        //[Route("shoppingcart/index")]
        public IActionResult Index()
        {
            //List<int> lsProductId=new List<int>();
            //var lsGiohang = GioHang;
            //return View(GioHang);
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            return View(cart);
        }
    }
}
