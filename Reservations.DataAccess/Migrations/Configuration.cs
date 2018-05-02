using System.Collections.Generic;
using Reservations.Core.Entities;
using Reservations.DataAccess.DataContext;

namespace Reservations.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Reservations.DataAccess.DataContext.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Reservations.DataAccess.DataContext.EfDbContext context)
        {
            AddContactTypeDefault(context);
        }

        private static void AddContactTypeDefault(EfDbContext context)
        {
            var contactTypes = new List<ContactType>
            {
                new ContactType() {Description = "Actor"},
                new ContactType() {Description = "Economic"},
                new ContactType() {Description = "Engineer"},
                new ContactType() {Description = "Student"},
                new ContactType() {Description = "Teacher"}
            };
            contactTypes.ForEach(d =>
            {
                if (context.ContactTypes.FirstOrDefault(t => d.Description.Equals(t.Description)) == null)
                {
                    context.ContactTypes.Add(d);
                }
            });
        }
    }
}