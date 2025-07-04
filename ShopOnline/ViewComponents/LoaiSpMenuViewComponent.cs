using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.Repository;
namespace ShopOnline.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;
        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaisps = _loaiSp.GetAll().OrderBy(p=>p.CatId);
            return View(loaisps);
        }
    }

}
