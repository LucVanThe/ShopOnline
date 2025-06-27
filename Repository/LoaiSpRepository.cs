using ShopOnline.Models;
namespace ShopOnline.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly WebBanhangDbContext _context;
        public LoaiSpRepository(WebBanhangDbContext context)
        {
            _context = context;
        }
        public Category Add(Category loaisp)
        {
            _context.Categories.Add(loaisp);
            _context.SaveChanges();
            return loaisp;
        }

        public Category Delete(int Maloaisp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
          
            return _context.Categories.ToList();
        }

        public Category GetById(int Maloaisp)
        {
           
            return _context.Categories.Find(Maloaisp);
        }

        public Category Update(Category loaisp)
        {
            _context.Categories.Update(loaisp);
            _context.SaveChanges();
            return loaisp;
        }

      
    }
}
