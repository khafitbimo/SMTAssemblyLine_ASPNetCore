using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMTAssemblyLineWebApp.Models.SMTViewModels
{
    public class StationCountDataToday
    {
        public int STATION_ID { get; set; }
        public string STATION_NAME { get; set; }
        public int STATION_COUNT_TODAY { get; set; }
        public double STATION_AVG_COUNT_TODAY { get; set; }
        public double STATION_MIN_COUNT_TODAY { get; set; }
        public double STATION_MAX_COUNT_TODAY { get; set; }
    }
}
