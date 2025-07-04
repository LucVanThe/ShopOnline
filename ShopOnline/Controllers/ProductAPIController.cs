using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    [ApiController] // Move the attribute to the class declaration
    [Route("api/[controller]")]
    public class ProductAPIController : ControllerBase
    {
       
        private readonly WebBanhangDbContext db;
        public ProductAPIController(WebBanhangDbContext context)
        {
            db = context;
        }
        [HttpGet]
        public IEnumerable<SanPham> GetAllProduct()
        {
            var product = (from p in db.Products
                           where p.Active == true
                           select new SanPham
                           {
                               productId = p.ProductId,
                               catId = p.CatId,
                               productName = p.ProductName,
                               productImage = p.Thumb,
                               productDescription = p.Description,
                               sortDescription = p.ShortDesc,
                               isActive = p.Active,
                               quantity = p.UnitInStock,
                               productDiscount = p.Discount,
                               productPrice = p.Price
                           }
                           ).ToList();
            return product;
        }
        [HttpGet("{CatId}")]
        public IEnumerable<SanPham> GetProductByCatId(int CatId)
        {
            var product = (from p in db.Products
                           where p.Active == true && p.CatId==CatId|| CatId== 0
                           select new SanPham
                           {
                               productId = p.ProductId,
                               catId = p.CatId,
                               productName = p.ProductName,
                               productImage = p.Thumb,
                               productDescription = p.Description,
                               sortDescription = p.ShortDesc,
                               isActive = p.Active,
                               quantity = p.UnitInStock,
                               productDiscount = p.Discount,
                               productPrice = p.Price
                           }
                           ).ToList();
            return product;
        }
    }
}
