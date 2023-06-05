using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class CreditCard
{
    public int Id { get; set; }

    public string CreditCardNumber { get; set; }

    public int Cvv { get; set; }

    public DateTime ExpireDate { get; set; }

    public int CreditCardTypeId { get; set; }

    public bool IsContactless { get; set; }

    public int CreditCardPassword { get; set; }

    public decimal CreditCardLimit { get; set; }

    public decimal CreditCardBalance { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime PaymentDueDate { get; set; }
}
