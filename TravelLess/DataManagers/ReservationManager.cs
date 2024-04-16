using Microsoft.EntityFrameworkCore;
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

        private DataContext db;

        public ReservationManager()
        {
            db = new DataContext();
            Reservations = db.Reservations.Include(p => p.Flight).ToList();
        }

        public void CreateReservation(Flight flight, string name, string citizenship)
        {
            Reservation reservation = new Reservation();
            reservation.Name = name;
            reservation.Citizenship = citizenship;
            reservation.flightId = flight.Id;
            reservation.Code = reservation.ReservationCode();
            db.Add(reservation);
            db.SaveChanges();
        }
        public List<Reservation> FindReso(string searchCode, string searchAir, string searchName)
        {
            if (searchAir == "" && searchCode == "" && searchAir == "")
            {
                return Reservations;
            }
            return db.Reservations.Where(r => r.Code == searchCode).Where(r => r.Airline == searchAir).Where(r => r.Name == searchName).ToList();
        }
        public void UpdateReservation(Reservation res, string name, string citizenship, bool active)
        {
            res.Name = name;
            res.Citizenship = citizenship;
            res.Active = active;
            db.Reservations.Update(res);
            db.SaveChanges();
        }
    }
}
