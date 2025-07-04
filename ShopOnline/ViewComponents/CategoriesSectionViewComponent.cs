using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
namespace ShopOnline.ViewComponents
{
    public class CategoriesSectionViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public CategoriesSectionViewComponent(WebBanhangDbContext context)
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
