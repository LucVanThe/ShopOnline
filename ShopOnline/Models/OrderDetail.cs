using System;
using System.Collections.Generic;

namespace ShopOnline.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public string? OrderNumber { get; set; }

    public int? Quantity { get; set; }

    public double? Discount { get; set; }

    public double? Total { get; set; }

    public DateTime? ShipDate { get; set; }

    public virtual Order? Order { get; set; }
}
