using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.ViewModel;

namespace ShopOnline.Controllers
{
    public class ShopController : Controller
    {
        WebBanhangDbContext db = new WebBanhangDbContext();
        public IActionResult Index()
        {
           
            var listSP=db.Products
                .Where(p => p.Active == true && p.Discount > 0)
                .OrderByDescending(p => p.ProductId)
                
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
                }).ToList();
            return View(listSP);
        }
    }
}
