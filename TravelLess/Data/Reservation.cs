using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelLess.Data
{
    internal class Reservation
    {
        public string Code;
        public string FlightCode => Flight.Code;
        public string Airline => Flight.Airline;
        public string Name { get; set; }
        public string Citizenship { get; set; }
        public double Cost => Flight.Price;
        public bool Active { get; set; } = true;
        public Flight Flight { get; set; }

        /// <summary>
        /// Generates a new reservation code for the reservation
        /// </summary>
        public string ReservationCode()
        {
            Random rng = new Random();
            int code = rng.Next(0, 100000);
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return letters[rng.Next(letters.Length)] + code.ToString();
        }
       
    }
}
