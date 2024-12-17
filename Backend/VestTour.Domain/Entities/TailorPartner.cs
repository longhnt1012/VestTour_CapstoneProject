using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class TailorPartner
{
    public int TailorPartnerId { get; set; }

    public int StoreId { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<ProcessingTailor> ProcessingTailors { get; set; } = new List<ProcessingTailor>();

    public virtual Store Store { get; set; } = null!;

    public virtual User? User { get; set; }
}
