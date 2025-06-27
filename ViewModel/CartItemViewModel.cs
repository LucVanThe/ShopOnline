using ShopOnline.Models;

namespace ShopOnline.ViewModel
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public double? discount { get; set; }

        // Fix for CS0266 and CS8629: Ensure null checks and explicit conversion
        public double totalPrice
        {
            get
            {
                if (Price.HasValue && discount.HasValue)
                {
                    return Price.Value * (1 - (discount.Value / 100));
                }
                return 0; // Default value if Price or discount is null
            }
        }
    }
}
