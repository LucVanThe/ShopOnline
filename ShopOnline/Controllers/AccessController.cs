using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.ViewModel;

namespace ShopOnline.Controllers
{
    public class AccessController : Controller
    {
        private readonly WebBanhangDbContext _context;
        public AccessController(WebBanhangDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("FullName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "CheckOut");
            }
        }
        [HttpPost]
        public IActionResult Login(Account user)
        {
            if (HttpContext.Session.GetString("FullName") == null)
            {
                var account = _context.Accounts.Where(x => x.Email.Equals(user.Email)).FirstOrDefault();
                if (account == null)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại.");
                    return View();
                }
                if (account != null && account.Password!=null && BCrypt.Net.BCrypt.Verify(user.Password, account.Password))
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, account.FullName),
                            new Claim(ClaimTypes.Role, account.RoleId.ToString())
                        };

                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync("MyCookieAuth", principal);
                    HttpContext.Session.SetString("FullName", account.FullName.ToString());


                    if (account.RoleId == 1)
                    {
                        return RedirectToAction("Index", "AdminProducts", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "CheckOut");
                    }

                }

            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult RegisterAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAccount(AccountVM model)
        {
            if (model.Password != model.RePassword)
            {
                ModelState.AddModelError("RePassword", "Mật khẩu xác nhận không đúng.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user= new Account
                {
                    FullName = model.FullName,
                    Phone = model.Phone,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // In a real application, you should hash the password
                    Salt = Guid.NewGuid().ToString(),
                    Active = true, // Set the account as active by default
                    LastLogin = DateTime.Now,// Set the last login time to now
                    CreateDate = DateTime.Now,
                    RoleId = 2// Generate a salt for password hashing

                };
               
                // Default role for new users
                _context.Accounts.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đăng ký tài khoản thành công!";
                return RedirectToAction("Login", "Access");
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
