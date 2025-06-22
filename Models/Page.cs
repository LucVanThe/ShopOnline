using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models;

public partial class Page
{
    public int PageId { get; set; }
    [Display(Name ="Tên")]
    public string? PageName { get; set; }
    [Display(Name = "Nội dung")]
    
    public string? Contents { get; set; }
    [Display(Name = "Ảnh")]
    public string? Thumb { get; set; }
    [Display(Name = "Công khai")]
    public bool? Publish { get; set; }

    public string? Title { get; set; }

    public string? MetaDesc { get; set; }

    public string? MetaKey { get; set; }

    public string? Alias { get; set; }
    [Display(Name = "Ngày tạo")]
    public DateTime? CreatedDate { get; set; }

    public int? Ordering { get; set; }
}
