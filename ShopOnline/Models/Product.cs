using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models;

public partial class Product
{
    public int ProductId { get; set; }
    [Display(Name ="Tên sản phẩm")]
    public string? ProductName { get; set; }

    public string? ShortDesc { get; set; }
    [Display(Name ="Mô tả")]
    public string? Description { get; set; }

    public int? CatId { get; set; }
    [Display(Name ="Giá")]
    public double? Price { get; set; }
    [Display(Name ="Giảm giá(%)")]
    public double? Discount { get; set; }
    [Display(Name ="Ảnh")]
    public string? Thumb { get; set; }

    public string? Video { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
    [Display(Name ="Bán chạy")]
    public bool? BestSellers { get; set; }

    public bool? HomeFlag { get; set; }

    public bool? Active { get; set; }

    public string? Tags { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? MetaDesc { get; set; }

    public string? MetaKey { get; set; }
    [Display(Name ="Số lượng")]
    public int? UnitInStock { get; set; }

    public virtual ICollection<AttributesPrice> AttributesPrices { get; set; } = new List<AttributesPrice>();

    public virtual Category? Cat { get; set; }
}
