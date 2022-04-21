using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace Hotel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ClientList> ClientLists { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomNumber> RoomNumbers { get; set; }
        public DbSet<RoomClass> RoomClasses { get; set; }
        public DbSet<Gender> Genders { get; set; }

        public ApplicationContext()
        {
            try
            {
              //  Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=HotelDB;Trusted_Connection=True;");
        }
    }
}
