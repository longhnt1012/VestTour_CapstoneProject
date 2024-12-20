﻿using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Style
{
    public int StyleId { get; set; }

    public string? StyleName { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<StyleOption> StyleOptions { get; set; } = new List<StyleOption>();
}
