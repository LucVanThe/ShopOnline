using Microsoft.AspNetCore.Mvc;
using X.PagedList.Mvc.Core;
using ShopOnline.Models;
using X.PagedList;
using System.Drawing.Printing;
namespace ShopOnline.ViewComponents
{
    public class ProductArrivalViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public ProductArrivalViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int? CatId = null, int page = 1)
        {
            int PageSize = 8; 
            var query = _context.Products
                .Where(p => p.Active == true && (CatId == 0 || p.CatId == CatId));

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            var products = query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.CatId = CatId;

            return View(products);
        }
    }
   
}
