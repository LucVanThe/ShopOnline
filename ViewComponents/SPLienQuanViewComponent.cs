using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
namespace ShopOnline.ViewComponents
{
    public class SPLienQuanViewComponent : ViewComponent
    {
        WebBanhangDbContext db = new WebBanhangDbContext();
        public IViewComponentResult Invoke(int CatId, int ProductId)
        {
            var relatedProducts = db.Products
                .Where(p => p.CatId == CatId && p.Active == true && p.ProductId != ProductId)
                .OrderBy(p => p.ProductName)
                .Take(4)
                .ToList();
            return View(relatedProducts);
        }
    }
}
