using System;
using System.Collections.Generic;

namespace BatuBankClassLibary.Entities;

public partial class UserAccountObserver
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? AccountId { get; set; }

    public DateTime ActionTakenDateTime { get; set; }

    public decimal OldAccountBalance { get; set; }

    public decimal NewAccountBalance { get; set; }

    public bool IsRisky { get; set; }

    public string Report { get; set; }

}
