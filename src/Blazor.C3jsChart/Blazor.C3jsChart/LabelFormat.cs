using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blazor.C3js.Chart
{
    [DataContract]
    public enum LabelFormat
    {
        [EnumMember(Value = "value")]
        Value,
        [EnumMember(Value = "ratio")]
        Ratio,
        [EnumMember(Value = "id")]
        Id
    }
}