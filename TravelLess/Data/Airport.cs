using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelLess.Data
{
    public class Airport
    {
        [Key]
        public string AirportId { get; set; } //YYC, YVR, etc.
        public string AirportName { get; set; } // Calgary International Airport
    }
}
