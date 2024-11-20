using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string? Comment { get; set; }

    public int? Rating { get; set; }

    public string? Response { get; set; }

    public DateOnly? DateSubmitted { get; set; }
    public DateOnly? DateResponse { get; set; }
    public int? UserId { get; set; }

    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
    public virtual Product? Product { get; set; }
}
