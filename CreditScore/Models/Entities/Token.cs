using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class Token
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string TokenValue { get; set; } = null!;

    public DateTime ExpiryDateTime { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public virtual User? User { get; set; }
}
