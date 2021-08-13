using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusadaAircraftSpotting.Data
{
    public class AircraftSightings
    {

        public int Id { get; set; }
        public string AircraftMake { get; set; }
        public string AircraftModel { get; set; }
        public string AircraftRegistration { get; set; }
        public string LocationSpotted { get; set; }
      
        public string Notes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string ImageLocation { get; set; }
    }
}
