using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelLess.Data;

namespace TravelLess.DataManagers
{
    internal class FlightManager
    {
        public List<Flight> Flights { get; }
        public int LoadedFlights => Flights.Count;

        public List<string> AirportCodes { get; }

        private DataContext db;
        public FlightManager()
        {
            db = new DataContext();
            Flights = db.Flights.ToList();

            //Loads data from text file
            if (Flights.Count == 0)
            {
                Flights = new List<Flight>();
                string dir = Assembly.GetExecutingAssembly().Location + "../../../../../../../../flights.csv";
                Console.WriteLine(dir);
                var lines = File.ReadAllLines(dir).ToList();
                foreach (var line in lines)
                {
                    var data = line.Split(",");
                    Flight flight = new Flight();
                    flight.Code = data[0];
                    flight.Airline = data[1];
                    flight.FromAirport = data[2];
                    flight.ToAirport = data[3];
                    flight.DayofWeek = data[4];
                    flight.DepartureTime = data[5];
                    flight.Capacity = int.Parse(data[6]);
                    flight.Price = double.Parse(data[7]);
                    Flights.Add(flight);
                    db.Flights.Add(flight);
                }
                db.SaveChanges();
            }

            AirportCodes = Flights.Select(flight => flight.FromAirport).Distinct().ToList();
            AirportCodes.AddRange(Flights.Select(flight => flight.ToAirport).Distinct());
            AirportCodes = AirportCodes.Distinct().ToList();
        }

        /// <summary>
        /// Finds a flight according to search parameters
        /// </summary>
        /// <param name="from">The from airport, YVR for example</param>
        /// <param name="to">The to airport, YYC for example</param>
        /// <param name="dayofweek">The day of the week to depart on, Thursday for example</param>
        public List<Flight> FindFlight(string from, string to, string dayofweek)
        {
            var found = Flights.Where(flight =>
                 flight.FromAirport.Equals(from, StringComparison.OrdinalIgnoreCase) &&
                 flight.ToAirport.Equals(to, StringComparison.OrdinalIgnoreCase) &&
                 flight.DayofWeek.Equals(dayofweek, StringComparison.OrdinalIgnoreCase))
                 .ToList();
            return found;
        }
    }
}