using System;
using System.Runtime.Serialization;

namespace Blazor.C3js.Chart
{
    [Serializable]
    internal class InvalidChartOptions : Exception
    {
        public InvalidChartOptions()
        {
        }

        public InvalidChartOptions(string message) : base(message)
        {
        }

        public InvalidChartOptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidChartOptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}