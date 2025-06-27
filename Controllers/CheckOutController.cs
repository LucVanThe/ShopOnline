using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
