using SMTAssemblyLineWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMTAssemblyLineWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initializer(SMTContext context)
        {
            context.Database.EnsureCreated();

            if (context.Stations.Any())
            {
                return;
            }

            var stations = new Station[]
            {
                new Station{STATION_NAME="Drill"},
                new Station{STATION_NAME="Lathe"}
               
            };

            foreach (Station s in stations)
            {
                context.Stations.Add(s);

            }
            context.SaveChanges();

        }
    }
}
