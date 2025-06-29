
using System.ComponentModel.DataAnnotations;
using ShopOnline.Models;
namespace ShopOnline.ViewModel
{
    public class AccountVM
    {
        [Display(Name ="Họ tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string? FullName { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string? Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string? Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        public string? RePassword { get; set; }    
        
       
    }
}
