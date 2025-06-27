using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Authentication;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
    }
}
