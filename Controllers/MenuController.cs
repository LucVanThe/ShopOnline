using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MenuArrivals()
        {
            return View();
        }
    }
}
