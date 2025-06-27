using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
namespace ShopOnline.ViewComponents
{
    public class MenuLeftViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public MenuLeftViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
