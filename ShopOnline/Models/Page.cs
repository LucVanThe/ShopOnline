﻿using System;
using System.Collections.Generic;

namespace ShopOnline.Models;

public partial class Page
{
    public int PageId { get; set; }

    public string? PageName { get; set; }

    public string? Contents { get; set; }

    public string? Thumb { get; set; }

    public bool? Publish { get; set; }

    public string? Title { get; set; }

    public string? MetaDesc { get; set; }

    public string? MetaKey { get; set; }

    public string? Alias { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Ordering { get; set; }
}
