using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class StyleOption
{
    public int StyleOptionId { get; set; }

    public int? StyleId { get; set; }

    public string? OptionType { get; set; }

    public string? OptionValue { get; set; }

    public virtual Style? Style { get; set; }
}
