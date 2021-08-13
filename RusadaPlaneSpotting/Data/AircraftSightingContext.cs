using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusadaAircraftSpotting.Data
{
    public class AircraftSightingContext : DbContext
    {
        #region Constructor
        public AircraftSightingContext(DbContextOptions<AircraftSightingContext> options) : base(options)
        {

        }
        #endregion


        public DbSet<AircraftSightings> AircraftSightings { get; set; } 
    }



}
