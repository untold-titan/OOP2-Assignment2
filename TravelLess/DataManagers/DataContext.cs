using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelLess.Data;

namespace TravelLess.DataManagers
{
    public class DataContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DataContext() 
        {
            SQLitePCL.Batteries_V2.Init();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "blogs.db3");
            options.UseSqlite($"Filename={dbPath}");

        }
    }
}
