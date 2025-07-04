using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.ViewModel;
namespace ShopOnline.ViewComponents
{
    public class SanPhamMoiViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public SanPhamMoiViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sanPhamMoi = _context.Products
                .Where(c => c.Active == true)
                .OrderByDescending(c => c.DateCreated)              
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
                }).Take(3)
                .ToList();
            return View(sanPhamMoi);
        }
    }
    public class SanPhamMoiNhatViewComponent : ViewComponent
    {
        private readonly WebBanhangDbContext _context;
        public SanPhamMoiNhatViewComponent(WebBanhangDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sanPhamMoi = _context.Products
                .Where(c => c.Active == true)
                .OrderByDescending(c => c.DateCreated)
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
                }).Take(6)
                .ToList();
            return View(sanPhamMoi);
        }
    }
}
