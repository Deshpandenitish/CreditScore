using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class AuditTrail
{
    public int AuditId { get; set; }

    public int? UserId { get; set; }

    public string? ActivityDescription { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? TableName { get; set; }

    public virtual User? User { get; set; }
}
