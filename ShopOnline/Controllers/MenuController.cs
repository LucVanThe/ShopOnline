using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class MenuController : Controller
    {
        private readonly WebBanhangDbContext _context;
        public MenuController(WebBanhangDbContext context)
        {
            _context = context;
        }
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
