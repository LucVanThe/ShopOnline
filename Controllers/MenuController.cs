using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class MenuController : Controller
    {
        WebBanhangDbContext _context = new WebBanhangDbContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MenuArrivals()
        {
            var categories = _context.Categories.ToList();
            return PartialView(categories);
        }
    }
}
