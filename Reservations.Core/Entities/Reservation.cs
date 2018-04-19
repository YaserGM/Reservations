namespace Reservations.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Reservation : DataBaseEntity
    {
        [Required]
        public int Rating { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public string Descriptions { get; set; }

        [Required]
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }

    }
}