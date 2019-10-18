using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMTAssemblyLineWebApp.Models
{
    public class Station
    {
        public int ID { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name ="Station Name")]
        public string STATION_NAME { get; set; }


    }
}
