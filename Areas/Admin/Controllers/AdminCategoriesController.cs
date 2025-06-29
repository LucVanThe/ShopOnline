using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class AdminCategoriesController : Controller
    {
        private readonly WebBanhangDbContext _context;

        public AdminCategoriesController(WebBanhangDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Category category, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {

                if (Avatar != null && Avatar.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(Avatar.FileName);
                    var extension = Path.GetExtension(Avatar.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Category", newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Avatar.CopyToAsync(stream);
                    }

                    category.Thumb = newFileName;

                }
               
                category.Alias = Helper.Utilities.GenerateAlias(category.CatName);
                _context.Add(category);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Category category, IFormFile Avatar)
        {
            if (id != category.CatId)
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
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Category", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Avatar.CopyToAsync(stream);
                        }

                        category.Thumb = newFileName;

                    }
                   
                    category.Alias = Helper.Utilities.GenerateAlias(category.CatName);
                    _context.Update(category);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
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
            return View(category);
        }

        // GET: Admin/AdminCategories/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Categories
        //        .FirstOrDefaultAsync(m => m.CatId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost]
        
        public IActionResult Delete(int id)
        {
            var category =  _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

             _context.SaveChanges();
            return Json(new { success = true, message = "Xóa thành công" });
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CatId == id);
        }
    }
}
