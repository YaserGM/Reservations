using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;
using Reservations.Core;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.DataContext.Configurations
{
    public class ContactEntityConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactEntityConfiguration()
        {
            this.Property(t => t.Name).IsRequired()
                .HasMaxLength(50);

            this.HasIndex(t => t.Name).IsUnique();

            this.Property(t => t.PhoneNumber).IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Birthdate).IsRequired();

            this.Property(t => t.ContactTypeId).IsRequired();

            this.HasRequired(t => t.ContactType);

            this.HasMany(t => t.Reservations).WithRequired(t => t.Contact);
        }
    }
}