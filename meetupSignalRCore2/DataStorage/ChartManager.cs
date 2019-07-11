using meetupSignalRCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meetupSignalRCore2.DataStorage
{
    public static class ChartManager
    {
        public static List<ChartModel> GetData()
        {
            var r = new Random();
            return new List<ChartModel>()
            {
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data 1" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data 2" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data 3" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data 4" }
            };
        }
    }
}