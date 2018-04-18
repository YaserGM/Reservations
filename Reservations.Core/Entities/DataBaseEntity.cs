using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservations.Core.Definitions;

namespace Reservations.Core.Entities
{
    public class DataBaseEntity : IDataBaseEntity
    {
        public int Id { get; set; }
    }
}
