using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.C3jsChart
{
    public class PieOptions
    {
        public bool ShowLabel { get; set; } = true;
        public LabelFormat LabelFormat { get; set; }
        public float LabelThreshod { get; set; } = 0.05f;
        public bool Expand { get; set; } = true;
    }
}
