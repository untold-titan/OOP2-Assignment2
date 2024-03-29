using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelLess.Components.Pages;
using TravelLess.Data;

namespace TravelLess.DataManagers
{
    internal class ReservationManager
    {
        public List<Reservation> Reservations { get; set; }


        public ReservationManager()
        {
            Reservations = new List<Reservation>();
            if (File.Exists(Assembly.GetExecutingAssembly().Location + "../../../../../../../../reservations.csv"))
            {
                var lines = File.ReadAllLines(Assembly.GetExecutingAssembly().Location + "../../../../../../../../reservations.csv");
                FlightManager flights = new FlightManager(); // Used to grab the flight associated with the reservation
                foreach (var line in lines)
                {
                    Reservation reservation = new Reservation();
                    var data = line.Split(",");
                    reservation.Name = data[0];
                    reservation.Citizenship = data[1];
                    reservation.Flight = flights.Flights.FirstOrDefault(f => f.Code == data[2]);
                    reservation.Code = data[3];
                    Reservations.Add(reservation);
                }
            }
        }

        public void CreateReservation(Flight flight, string name, string citizenship)
        {
            Console.WriteLine("Data Received: " + name + "   " + citizenship + "   " + flight.Code);
            Reservation reservation = new Reservation();
            reservation.Name = name;
            reservation.Citizenship = citizenship;
            reservation.Flight = flight;
            reservation.Code = reservation.ReservationCode();
            Reservations.Add(reservation);
            SaveReservations();
        }
        public List<Reservation> FindReso(string searchCode, string searchAir, string searchName)
        {

            List<Reservation> SearchedReso = new List<Reservation>();
            foreach (Reservation reservation in Reservations)
            {
                if (searchAir == "" && searchCode == "" && searchAir == "")
                {
                    return Reservations;
                }

                if (reservation.Code == searchCode ||
                    reservation.Airline == searchAir ||
                    reservation.Name == searchName)
                {
                    SearchedReso.Add(reservation);
                }

            }
            return SearchedReso;
        }


        public void UpdateReservation(Reservation res, string name, string citizenship, bool active)
        {
            res.Name = name;
            res.Citizenship = citizenship;
            res.Active = active;
            string new_Name = "";
            string new_Cit = "";
            name.Replace(name, new_Name);
            citizenship.Replace(citizenship, new_Cit);
            SaveReservations();
        }

        public void SaveReservations()
        {
            List<string> lines = new();
            foreach (Reservation reservation in Reservations)
            {
                lines.Add(reservation.Name + "," + reservation.Citizenship + "," + reservation.Flight.Code + "," + reservation.Code);
            }
            File.WriteAllLines(Assembly.GetExecutingAssembly().Location + "../../../../../../../../reservations.csv", lines.ToArray());
        }
    }
}
