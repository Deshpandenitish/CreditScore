using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public virtual ICollection<AuditTrail> AuditTrails { get; } = new List<AuditTrail>();

    public virtual ICollection<CreditScore> CreditScores { get; } = new List<CreditScore>();

    public virtual ICollection<Document> Documents { get; } = new List<Document>();

    public virtual ICollection<FinancialDetail> FinancialDetails { get; } = new List<FinancialDetail>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual ICollection<Token> Tokens { get; } = new List<Token>();
}
