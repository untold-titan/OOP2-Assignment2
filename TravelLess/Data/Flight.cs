using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelLess.Data
{
    public class Flight
    {
        public int Id { get; set; }
        public string Code { get; set; } // TB-4012 
        public string Airline { get; set; } // Air Canada
        public string FromAirport { get; set; } // YYC, should be the airport code, not the name
        public string ToAirport { get; set; } // YVR, should be the airport code, not the name
        public string DayofWeek { get; set; } // Thursday
        public string DepartureTime { get; set; } // 19:30, 24hr clock
        public double Price { get; set; } // 5100.00, expensive flights lol
        public int Capacity { get; set; } // I think
    }
}
