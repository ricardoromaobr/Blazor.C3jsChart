using System;
using System.Runtime.Serialization;

namespace Blazor.C3js.Chart
{
    [Serializable]
    internal class DatasetAlreadyInDatasetsException : Exception
    {
        public DatasetAlreadyInDatasetsException()
        {
        }

        public DatasetAlreadyInDatasetsException(string message) : base(message)
        {
        }

        public DatasetAlreadyInDatasetsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatasetAlreadyInDatasetsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}