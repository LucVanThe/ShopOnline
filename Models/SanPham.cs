namespace ShopOnline.Models
{
    public class SanPham
    {
        public int productId { get; set; }
        public int? catId { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }
        public string productDescription { get; set; }
        public string sortDescription { get; set; }
        public bool? isActive { get; set; }
        public int? quantity { get; set; }
        public double? productDiscount { get; set; }
        public double? productPrice { get; set; }
        public double? totalPrice => productPrice - (productPrice * (productDiscount / 100));
    }
}
