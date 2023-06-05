using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class TransactionType
{
    public int Id { get; set; }

    public string TransactionTypeName { get; set; } = null!;
}
