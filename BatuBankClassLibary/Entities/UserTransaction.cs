using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class UserTransaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TransactionTypeId { get; set; }

    public string Report { get; set; } = null!;

    public decimal NetIncome { get; set; }
}
