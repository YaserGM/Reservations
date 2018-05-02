using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration;
using Reservations.Core;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.DataContext.Configurations
{
    public class ReservationEntityConfiguration : EntityTypeConfiguration<Reservation>
    {
        public ReservationEntityConfiguration()
        {
            this.Property(t => t.RanKing).IsRequired();

            this.Property(t => t.CreateDate).IsRequired();

            this.Property(t => t.Descriptions).IsRequired();

            this.Property(t => t.Favorite).IsRequired();

            this.Property(t => t.ContactId).IsRequired();
        }
    }
}