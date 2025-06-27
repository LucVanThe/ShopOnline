using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
namespace ShopOnline.ViewComponents
{
    public class RecentNewsViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public RecentNewsViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var recentNews = _context.Pages
                .Where(p => p.Publish == true)
                .OrderByDescending(p => p.CreatedDate)
                .Take(3)
                .ToList();
            return View(recentNews);
        }
    }
}
