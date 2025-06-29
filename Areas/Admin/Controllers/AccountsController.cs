using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using X.PagedList.Extensions;

namespace ShopOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class AccountsController : Controller
    {
        private readonly WebBanhangDbContext _context;

        public AccountsController(WebBanhangDbContext context)
        {
            _context = context;
        }
       
        // GET: Admin/Accounts
        public IActionResult Index(string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var accounts = _context.Accounts.Include(a => a.Role).AsQueryable();
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a => a.FullName.Contains(searchString) || a.Email.Contains(searchString));
            }
            var tk = accounts.ToPagedList(pageNumber, pageSize);
            return View(tk);
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                account.CreateDate = DateTime.Now;
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }           
            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewBag.RoleId = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName");
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
           // ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var account =  _context.Accounts.Find(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

             _context.SaveChanges();
            return Json(new { success = true, message = "Xóa thành công" });
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
