using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.ViewComponents
{
    public class NumberCartViewComponent : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
