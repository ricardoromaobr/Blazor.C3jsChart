using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.C3jsChart
{
    public class ChartData<T>
    {
        private readonly List<DataSet<T>> _datasets;
        public ChartData()
        {
            _datasets = new List<DataSet<T>>();
        }
        public IEnumerable<DataSet<T>> Datasets => _datasets;
        public void AddDataset(DataSet<T> dataset)
        {
            _datasets.Add(dataset);
        }
    }
}
