using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMTAssemblyLineWebApp.Models
{
    public class StationCountPerHour
    {
        public int ID { get; set; }
        public int STATION_ID { get; set; }
        public DateTime COUNT_HOUR { get; set; }
        public int COUNT_IN_HOUR { get; set; }
    }
}
