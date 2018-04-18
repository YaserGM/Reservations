using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.DataContext
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("ReservationsDB")
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}