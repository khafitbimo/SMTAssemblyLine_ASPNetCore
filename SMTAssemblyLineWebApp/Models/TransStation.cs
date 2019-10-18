using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMTAssemblyLineWebApp.Models
{
    public class TransStation
    {
        public int ID { get; set; }
        public int STATION_ID { get; set; }
  
        public DateTime TIME_STAMP { get; set; }

        
    }
}
