using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Blazor.C3js.Chart
{
    [DataContract]
    public class PieOptions
    {
        [DataMember]
        public bool ShowLabel { get; set; } = true;
        [DataMember]
        public LabelFormat LabelFormat { get; set; }
        [DataMember]
        public float LabelThreshold { get; set; } = 0.05f;
        [DataMember]
        public bool Expand { get; set; } = true;
    }
}
