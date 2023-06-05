using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class UserCreditCard
{
    public int Id { get; set; }

    public int CreditCardId { get; set; }

    public int UserId { get; set; }
}
