using System;
using System.Collections.Generic;

namespace LapShop.Models;

public partial class VwItemCategory1
{
    public string ItemName { get; set; } = null!;

    public int ItemId { get; set; }

    public decimal SalesPrice { get; set; }

    public decimal PurchasePrice { get; set; }

    public string? Description { get; set; }

    public string? Gpu { get; set; }

    public string? HardDisk { get; set; }

    public string? Processor { get; set; }

    public int? RamSize { get; set; }

    public string? ScreenReslution { get; set; }

    public string? ScreenSize { get; set; }

    public int? OsId { get; set; }

    public string CategoryName { get; set; } = null!;
}
