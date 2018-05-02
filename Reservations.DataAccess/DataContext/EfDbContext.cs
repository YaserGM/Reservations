using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservations.Core;
using Reservations.Core.Entities;
using Reservations.DataAccess.DataContext.Configurations;

namespace Reservations.DataAccess.DataContext
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("ReservationsDB")
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ContactTypeEntityConfiguration());

            modelBuilder.Configurations.Add(new ContactEntityConfiguration());

            modelBuilder.Configurations.Add(new ReservationEntityConfiguration());

        }
    }
}