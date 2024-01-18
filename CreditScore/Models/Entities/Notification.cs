using System;
using System.Collections.Generic;

namespace CreditScore.Models.Entities;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? NotificationName { get; set; }

    public string? NoteStatus { get; set; }

    public DateTime? NotificationDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
