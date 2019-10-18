using Microsoft.EntityFrameworkCore;
using SMTAssemblyLineWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMTAssemblyLineWebApp.Data
{
    public class SMTContext : DbContext
    {
        public SMTContext(DbContextOptions<SMTContext> options) : base(options)
        {

        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<TransStation> TransStations { get; set; }
        public DbSet<StationCountPerHour> StationCountPerHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().ToTable("STATION");
            modelBuilder.Entity<TransStation>().ToTable("TRANS_STATION");
            modelBuilder.Entity<StationCountPerHour>().ToTable("STATION_COUNT_PER_HOUR");
        }


    }
}
