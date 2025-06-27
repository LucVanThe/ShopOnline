using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc.Core;

namespace ShopOnline.Controllers
{
    public class ProductsController : Controller
    {
        private readonly WebBanhangDbContext _context;
        public ProductsController(WebBanhangDbContext context)
        {
            _context = context;
        }
        // GET: ProductsController
        public IActionResult Index()

        {
           // int pageSize = 8;
          //  int pageNumber =page==null|| page < 1 ? 1 : page.Value;
            var products= _context.Products.Where(p=>p.Active==true).AsNoTracking().OrderBy(p=>p.ProductName).ToList();
           // var pageList = products.ToPagedList(pageNumber, pageSize);
            return View(products);
        }
        [HttpGet]
        public IActionResult LoadProductsByCategory(int CatId, int page = 1)
        {

            return ViewComponent("ProductArrival", new { CatId,page });
        }
        public IActionResult SanPhamTheoLoai(int CatId)
        {
            List<Product> listsp = _context.Products.Where(p => p.CatId == CatId || CatId == 0).ToList();
            return View(listsp);
        }
    }
}
