using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using X.PagedList;
namespace ShopOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class AdminCustomersController : Controller
    {
        private readonly WebBanhangDbContext _context;

        public AdminCustomersController(WebBanhangDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCustomers
        public IActionResult Index(int? page, string searchString)
        {
            var pageNumber = page == null|| page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var customers = _context.Customers
                .OrderByDescending(c => c.CreateDate).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(a => a.FullName.Contains(searchString) || a.Email.Contains(searchString));
            }
            PagedList<Customer> pagedCustomers = new PagedList<Customer>(customers, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(pagedCustomers); 
        }

        // GET: Admin/AdminCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/AdminCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                if (Avatar != null && Avatar.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(Avatar.FileName);
                    var extension = Path.GetExtension(Avatar.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Customers", newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Avatar.CopyToAsync(stream);
                    }

                    customer.Avatar = newFileName;
                    
                }
               
                customer.CreateDate = DateTime.Now;
                _context.Add(customer);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =  _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/AdminCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Customer customer, IFormFile Avatar)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Avatar != null && Avatar.Length > 0)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Avatar.FileName);
                        var extension = Path.GetExtension(Avatar.FileName);
                        string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Customers", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Avatar.CopyToAsync(stream);
                        }

                        customer.Avatar = newFileName;

                    }
                  
                    _context.Update(customer);
                     _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _context.Customers
        //        .FirstOrDefaultAsync(m => m.CustomerId == id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customer);
        //}

        // POST: Admin/AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (!string.IsNullOrEmpty(customer.Avatar))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Customers", customer.Avatar);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thành công" });
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
