using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class CreditScore
{
    public int CreditId { get; set; }

    public int? UserId { get; set; }

    public int CreditScore1 { get; set; }

    public decimal? DebtToIncomeRatio { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public virtual User? User { get; set; }
}
