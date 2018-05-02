using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reservations.Core.Entities
{
    public class ContactType : DataBaseEntity
    {
        public string Description { get; set; }

    }
}