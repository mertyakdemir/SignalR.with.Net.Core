using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chart.API.Models
{
    public class ChartPivotModel
    {
        public ChartPivotModel()
        {
            Counts = new List<int>();
        }

        public string ChartDate { get; set; }

        public List<int> Counts { get; set; }
    }
}
