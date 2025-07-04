using ShopOnline.Models;
namespace ShopOnline.Repository
{
    public interface ILoaiSpRepository
    {
        Category Add(Category loaisp);
        Category Update(Category loaisp);
        Category Delete(int Maloaisp);
        Category GetById(int Maloaisp);
        IEnumerable<Category> GetAll();
    }
}
