﻿using System;
using System.Collections.Generic;

namespace ShopOnline.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Slug { get; set; }

    public string? NameWithType { get; set; }

    public string? PathWithType { get; set; }

    public string? ParentCode { get; set; }

    public int? Levels { get; set; }
}
