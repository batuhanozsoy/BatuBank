using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class CreditCardType
{
    public int Id { get; set; }

    public string CreditCardTypeName { get; set; } = null!;
}
