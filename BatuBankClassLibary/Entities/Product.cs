using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public decimal? ProductSellPrice { get; set; }

    public decimal? ProductBuyPrice { get; set; }

    public DateTime? ProductDailyDataDateTime { get; set; }
}
