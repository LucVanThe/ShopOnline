using System.ComponentModel.DataAnnotations;
using ShopOnline.Models;
namespace ShopOnline.ViewModel
{
    public class MuaHangVM
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ giao hàng")]
        public string? Address { get; set; }      
        public int? PaymentId { get; set; }
        public string? Note { get; set; }





    }
}
