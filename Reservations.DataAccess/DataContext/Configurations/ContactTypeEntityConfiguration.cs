using System.Data.Entity.ModelConfiguration;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.DataContext.Configurations
{
    public class ContactTypeEntityConfiguration : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeEntityConfiguration()
        {
            this.Property(t => t.Description).IsRequired()
                .HasMaxLength(30);
            this.HasIndex(t => t.Description).IsUnique();
        }
    }
}