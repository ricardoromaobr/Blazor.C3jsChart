using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.C3jsChart
{
    public class DataSet<T>
    {
        /// <summary>
        /// Nome do dataset
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Serie de dados para  o dataset
        /// </summary>
        public IEnumerable<T> Datasouce { get; set; }

    }
}

