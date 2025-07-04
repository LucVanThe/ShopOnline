using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using X.PagedList.Mvc.Core;
using X.PagedList;
using X.PagedList.Extensions;
using System.Drawing.Printing;
using Azure;
using ShopOnline.ViewModel;
using ShopOnline.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
namespace ShopOnline.Controllers;

public class HomeController : Controller
{
    
    private readonly WebBanhangDbContext db;
    public HomeController(WebBanhangDbContext context)
    {
        db = context;
    }

    public IActionResult Index(int? page)

    {
         int pageSize = 8;
          int pageNumber =page==null|| page < 1 ? 1 : page.Value;
        var products = db.Products.Where(p => p.Active == true).AsNoTracking().OrderBy(p => p.ProductName).ToList();
         var pageList = products.ToPagedList(pageNumber, pageSize);
        return View(pageList);
    }
    public IActionResult ChiTietSanPham(int id)
    {
        var product = db.Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        var productDetai = new ProductDetailViewModel
        {
            productId = product.ProductId,
            catId = product.CatId,
            productName = product.ProductName,
            productImage = product.Thumb,
            productDescription = product.Description,
            sortDescription = product.ShortDesc,    
            isActive = product.Active,
            quantity = product.UnitInStock,
            productDiscount = product.Discount,
            productPrice =product.Price,
        };
        return View(productDetai);
    }
    public IActionResult SanPhamTheoLoai(int CatId,int? page)
    {
        int pageSize = 8;
        int pageNumber = page ?? 1;

        
        var query = db.Products
                      .Where(p => CatId == 0 || p.CatId == CatId)
                      .OrderBy(p => p.ProductName);

        var pageList = query.ToPagedList(pageNumber, pageSize);
        ViewBag.maloai = CatId;
        return View(pageList);
    }



}
