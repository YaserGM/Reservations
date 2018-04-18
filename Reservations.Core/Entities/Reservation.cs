using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reservations.Core.Entities
{
    public class Reservation : DataBaseEntity
    {
        [Required]
        public int Rating { get; set; }

        [Required]
        public DateTime CreatehDate { get; set; }

        [Required]
        public string Descriptions { get; set; }

        [Required]
        public int ContactId { get; set; }

        [Required]
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }

    }
}