using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using X.PagedList.Extensions;
namespace ShopOnline.ViewComponents
{
    public class CategoryArrivalViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public CategoryArrivalViewComponent(WebBanhangDbContext context)
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
