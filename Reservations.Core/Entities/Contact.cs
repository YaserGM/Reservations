using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reservations.Core.Entities
{
    public class Contact : DataBaseEntity
    {
        [Required] [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public String PhoneNumber { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Contacttype { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}