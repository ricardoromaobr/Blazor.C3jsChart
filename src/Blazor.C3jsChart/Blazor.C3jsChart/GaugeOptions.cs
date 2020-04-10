using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.C3js.Chart
{
    public class GaugeOptions
    {
        public float ShowLabel { get; set; }
        public LabelFormat LabelFormat { get; set; }
        public bool Expand { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Units { get; set; }
        public string Width { get; set; }
    }
}
