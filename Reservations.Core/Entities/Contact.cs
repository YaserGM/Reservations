using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reservations.Core.Entities
{
    public class Contact : DataBaseEntity
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthdate { get; set; }

        public int ContactTypeId { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}