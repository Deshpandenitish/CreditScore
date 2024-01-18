using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class Document
{
    public int DocumentId { get; set; }

    public int? UserId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public virtual User? User { get; set; }
}
