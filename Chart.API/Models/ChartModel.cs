using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chart.API.Models
{
    public enum ECity
    {
        Texas = 1,
        California = 2,
        NewYork = 3,
        Florida = 4,
        Alaska = 5
    }

    public class ChartModel
    {
        public int Id { get; set; }

        public ECity City { get; set; }

        public int Count { get; set; }

        public DateTime CDateTime { get; set; }
    }
}
