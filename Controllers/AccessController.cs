using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;

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
            if(HttpContext.Session.GetString("FullName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult Login(Account user)
        {
           if(HttpContext.Session.GetString("FullName") == null)
            {
                var account = _context.Accounts.Where(x => x.FullName.Equals(user.FullName) && x.Password.Equals
                (user.Password)).FirstOrDefault();
               
                if(account != null)
                {
                    HttpContext.Session.SetString("FullName", account.FullName.ToString());
                    if (account.RoleId == 1)
                    {
                        return RedirectToAction("Index", "AdminProducts", new {area= "Admin"});
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
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
    }
}
