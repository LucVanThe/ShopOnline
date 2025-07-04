using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.ViewModel;
using X.PagedList;
using X.PagedList.Extensions;
namespace ShopOnline.Controllers
{
    public class ShopController : Controller
    {
        private readonly WebBanhangDbContext db;
        public ShopController(WebBanhangDbContext context)
        {
            db = context;
        }
        public IActionResult Index(int? page, int CatId=0)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;
            var listSP = db.Products
                .Where(p => p.Active == true && (CatId==0 || p.CatId == CatId))
                .Select(p => new ProductViewModel
                {
                    productId = p.ProductId,
                    catId = p.CatId,
                    CatName = p.Cat.CatName, // Assuming Category is a navigation property
                    productName = p.ProductName,
                    productImage = p.Thumb,
                    productDescription = p.Description,
                    sortDescription = p.ShortDesc,
                    isActive = p.Active,
                    quantity = p.UnitInStock,
                    productDiscount = p.Discount,
                    productPrice = p.Price,
                });
            var sp = listSP.ToPagedList(pageNumber, pageSize);
            return View(sp);
        }
    }
}
