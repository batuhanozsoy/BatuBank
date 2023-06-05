using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class Account
{
    public int Id { get; set; }

    public string AccountName { get; set; } = null!;

    public int AccountTypeId { get; set; }

    public decimal AccountBalance { get; set; }

    public DateTime AccountCreationTime { get; set; }

    public bool İsBlocked { get; set; }

    public string Iban { get; set; } = null!;
}
