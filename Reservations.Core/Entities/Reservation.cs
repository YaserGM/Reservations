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
        public double RanKing { get; set; }

        public DateTime CreateDate { get; set; }

        public string Descriptions { get; set; }

        public bool Favorite { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}