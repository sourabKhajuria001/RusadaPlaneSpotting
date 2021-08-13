using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RusadaAircraftSpotting.Models
{
    public class AircraftSightingModel
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Display(Name = "Aircraft make")]
        public string AircraftMake { get; set; }
        [MaxLength(30)]
        [Display(Name = "Aircraft model")]
        public string AircraftModel { get; set; }
        [MaxLength(30)]
        [Display(Name = "Aircraft registration")]
        public string AircraftRegistration { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Location spotted")]
        public string LocationSpotted { get; set; }
        [MaxLength(50)]
        [Display(Name = "Additional notes")]
        public string Notes { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string ImageLocation { get; set; }

    }


}
