using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.ViewModel;
using X.PagedList.Extensions;
using ShopOnline.Helper;
namespace ShopOnline.Controllers
{
    public class BlogController : Controller
    {
        private readonly WebBanhangDbContext _context;
        public BlogController(WebBanhangDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page,string searchText)
        {
            var blogs = _context.Pages.Where(p => p.Publish == true);

            ViewBag.CurrentFilter = searchText;
            if (!string.IsNullOrEmpty(searchText))
            {
                string keyword = Helper.Utilities.RemoveDiacritics(searchText.ToLower());
                var blogList = blogs.ToList();
                // var blogList = blogs.ToList();
                blogList = blogList.Where(p =>p.PageName !=null &&
                    Helper.Utilities.RemoveDiacritics(p.PageName.ToLower()).Contains(keyword) ||p.Title!=null&&
                     Helper.Utilities.RemoveDiacritics(p.Title.ToLower()).Contains(keyword)).ToList();
                blogs = blogList.AsQueryable();
            }
            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    blogs = blogs.Where(a => EF.Functions.Like(a.PageName, $"%{searchText}%")|| EF.Functions.Like(a.PageName, $"%{searchText}%"));
            //}
            int pageSize = 6; 
            int pageNumber = page > 0 ? page : 1;          
            var tintuc=blogs.ToPagedList(pageNumber, pageSize);
            return View(tintuc);
        }
        [Route("/blogdetail/{alias}-{id}")]
        public IActionResult BlogDetail(string alias,int id)
        {
            var blog=_context.Pages.FirstOrDefault(p => p.PageId == id);
            if (blog == null)
            {
                return NotFound();
            }
            var blogDetail = new BlogDetailViewModel
            {
                PageId = blog.PageId,
                PageName = blog.PageName,
                Contents = blog.Contents,
                Thumb = blog.Thumb,
                Publish = blog.Publish,
                Title = blog.Title,
                Alias = blog.Alias,
                CreatedDate = blog.CreatedDate
            };
            
            return View(blogDetail);
        }
        
    }
}
