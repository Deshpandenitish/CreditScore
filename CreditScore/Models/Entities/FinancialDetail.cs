using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class FinancialDetail
{
    public int Fid { get; set; }

    public int? UserId { get; set; }

    public decimal? Income { get; set; }

    public decimal? Expenses { get; set; }

    public string? OtherFinancialInfo { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public virtual User? User { get; set; }
}
