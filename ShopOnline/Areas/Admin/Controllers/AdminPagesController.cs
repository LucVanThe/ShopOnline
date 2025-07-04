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
    public class AdminPagesController : Controller
    {
        private readonly WebBanhangDbContext _context;

        public AdminPagesController(WebBanhangDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPages
        public IActionResult Index(int? page, string searchString)
        {
            var pages = _context.Pages.AsQueryable();

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 15;

            if (!string.IsNullOrEmpty(searchString))
            {
                pages = pages.Where(a => a.PageName.Contains(searchString));
            }

            pages = pages.OrderByDescending(c => c.CreatedDate); 

            var pagedPage = new PagedList<Page>(pages, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.CurrentFilter = searchString;

            return View(pagedPage);
        }


        // GET: Admin/AdminPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin/AdminPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Page page, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                if (Avatar != null && Avatar.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(Avatar.FileName);
                    var extension = Path.GetExtension(Avatar.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Pages", newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Avatar.CopyToAsync(stream);
                    }

                    page.Thumb = newFileName;

                }
                page.Alias = Helper.Utilities.GenerateAlias(page.PageName);
                page.CreatedDate = DateTime.Now;
                _context.Add(page);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Admin/AdminPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Admin/AdminPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Page page, IFormFile Avatar)
        {
            if (id != page.PageId)
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
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Pages", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Avatar.CopyToAsync(stream);
                        }

                        page.Thumb = newFileName;

                    }
                    page.Alias = Helper.Utilities.GenerateAlias(page.PageName);
                    _context.Update(page);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
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
            return View(page);
        }

        // GET: Admin/AdminPages/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var page = await _context.Pages
        //        .FirstOrDefaultAsync(m => m.PageId == id);
        //    if (page == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(page);
        //}

        // POST: Admin/AdminPages/Delete/5
        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            var page =  _context.Pages.Find(id);
            if (!string.IsNullOrEmpty(page.Thumb))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Pages", page.Thumb);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            if (page != null)
            {
                _context.Pages.Remove(page);
            }
          
            _context.SaveChanges();
            return Json(new { success = true, message = "Xóa thành công" });
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.PageId == id);
        }
    }
}
