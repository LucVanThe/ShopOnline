using Microsoft.AspNetCore.Mvc;
using ShopOnline.Controllers;
using ShopOnline.Models;
using ShopOnline.ViewModel;
using X.PagedList;
using X.PagedList.Extensions;
namespace ShopOnline.ViewComponents
{
    public class LocSPViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public LocSPViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sp = _context.Products
                .Where(c => c.Active == true && c.Discount > 0)
                .Select(c => new ProductViewModel
                {
                    productId = c.ProductId,
                    catId = c.CatId,
                    productName = c.ProductName,
                    productImage = c.Thumb,
                    productDescription = c.Description,
                    sortDescription = c.ShortDesc,
                    isActive = c.Active,
                    quantity = c.UnitInStock,
                    productDiscount = c.Discount,
                    productPrice = c.Price
                });
            //int pageSize = 6;

           // var product = sp.ToPagedList(page, pageSize);
            return View(sp);
        }
    }
}
