using ShopOnline.Models;

namespace ShopOnline.HomeViewModel
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        
        public HomeViewModel()
        {
            Categories = new List<Category>();
           
        }
    }
}
