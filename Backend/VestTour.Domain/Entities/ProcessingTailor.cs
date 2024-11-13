using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class ProcessingTailor
{
    public int ProcessingId { get; set; }

    public string? StageName { get; set; }

    public int TailorPartnerId { get; set; }

    public string? Status { get; set; }

    public int OrderId { get; set; }

    public string? Note { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual TailorPartner TailorPartner { get; set; } = null!;
}
