using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Models;

public partial class TbItem
{
    public TbItem()
    {
        TbItemDiscounts = new HashSet<TbItemDiscount>();
        TbItemImages = new HashSet<TbItemImage>();
        TbPurchaseInvoiceItems = new HashSet<TbPurchaseInvoiceItem>();
        TbSalesInvoiceItems = new HashSet<TbSalesInvoiceItem>();
        Customers = new HashSet<TbCustomer>();
    }
    [ValidateNever]
    public int ItemId { get; set; }
    [Required(ErrorMessage = "Enter item name")]
    public string ItemName { get; set; } = null!;

    [Required(ErrorMessage = "Enter item sales price")]
    [DataType(DataType.Currency,ErrorMessage = "please enter currency")]
    [Range(50,100000,ErrorMessage ="Enter price from 50 to 100000")]
    public decimal SalesPrice { get; set; }

    [Required(ErrorMessage = "Enter item purchase price")]
    [DataType(DataType.Currency, ErrorMessage = "please enter currency")]
    [Range(50, 100000, ErrorMessage = "Enter price from 50 to 100000")]
    public decimal PurchasePrice { get; set; }
    [ValidateNever]
    public int CategoryId { get; set; }

    [ValidateNever]
    public string? ImageName { get; set; }
    [ValidateNever]
    public DateTime CreatedDate { get; set; }
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;
    [ValidateNever]
    public int CurrentState { get; set; }
    [ValidateNever]
    public string? UpdatedBy { get; set; }
    [ValidateNever]
    public DateTime? UpdatedDate { get; set; }
    [ValidateNever]
    public string? Description { get; set; }
    [ValidateNever]
    public string? Gpu { get; set; }
    [ValidateNever]
    public string? HardDisk { get; set; }
    [ValidateNever]
    public int? ItemTypeId { get; set; }
    [ValidateNever]
    public string? Processor { get; set; }

    [Required(ErrorMessage ="Enter ram size")]
    [Range(1,500,ErrorMessage = "Enter ram from 1 to 500")]
    public int RamSize { get; set; }

    public string? ScreenReslution { get; set; }

    public string? ScreenSize { get; set; }

    public string? Weight { get; set; }
    [ValidateNever]
    public int OsId { get; set; }

    public virtual TbCategory Category { get; set; } = null!;

    public virtual TbItemType? ItemType { get; set; }

    public virtual TbO Os { get; set; } = null!;

    public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; } = new List<TbItemDiscount>();

    public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();

    public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = new List<TbPurchaseInvoiceItem>();

    public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = new List<TbSalesInvoiceItem>();

    public virtual ICollection<TbCustomer> Customers { get; set; } = new List<TbCustomer>();
}
