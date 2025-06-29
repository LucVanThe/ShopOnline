using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Models.Authentication;
using X.PagedList;

namespace ShopOnline.Areas.Admin.Controllers
{
   
   
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class AdminProductsController : Controller
    {
        private readonly WebBanhangDbContext _context;

        public AdminProductsController(WebBanhangDbContext context)
        {
            _context = context;
        }
       
        // GET: Admin/AdminProducts
        public IActionResult Index(string searchString, int? page)
        {
            var product = _context.Products.Include(a => a.Cat).AsQueryable();
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(a => EF.Functions.Like(a.ProductName,$"%{searchString}%") || EF.Functions.Like(a.Cat.CatName, $"%{searchString}%"));
            }
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10; // Số lượng bản ghi trên mỗi trang
            PagedList<Product> pagedProducts = new PagedList<Product>(product, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(pagedProducts);
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewBag.CatId = new SelectList(_context.Categories.ToList(), "CatId", "CatName");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                if (Avatar != null && Avatar.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(Avatar.FileName);
                    var extension = Path.GetExtension(Avatar.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                       await Avatar.CopyToAsync(stream);
                    }

                    product.Thumb = newFileName;

                }
              
                product.Alias = Helper.Utilities.GenerateAlias(product.ProductName);
                product.DateCreated = DateTime.Now;
                product.DateModified = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CatId = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CatId = new SelectList(_context.Categories.ToList(), "CatId", "CatName");
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Product product, IFormFile Avatar)
        {
            if (id != product.ProductId)
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
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Avatar.CopyToAsync(stream);
                        }

                        product.Thumb = newFileName;

                    }
                   product.Alias = Helper.Utilities.GenerateAlias(product.ProductName);
                    product.DateModified = DateTime.Now;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewBag.CatId = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Cat)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost]
       
        public IActionResult Delete(int id)
        {
            var product =  _context.Products.Find(id);
            if (!string.IsNullOrEmpty(product.Thumb))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", product.Thumb);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            if (product != null)
            {
                _context.Products.Remove(product);
            }
           
            _context.SaveChanges();
            return Json(new { success = true, message = "Xóa thành công" });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
