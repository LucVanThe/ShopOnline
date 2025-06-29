using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.ViewModel;
using ShopOnline.Extension;

namespace ShopOnline.ViewComponents
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart");
            if (cart == null)
            {
                return Content("0");
            }
            return View(cart.Count.ToString());
        }
    }
}
