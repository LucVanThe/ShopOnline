using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;

namespace ShopOnline.ViewComponents
{

    public class TinTucThemViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public TinTucThemViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int id)
        {
            var recentNews = _context.Pages
                .Where(p => p.Publish == true && p.PageId!=id)               
                .Take(3)
                .ToList();
            return View(recentNews);
        }
    }
}
