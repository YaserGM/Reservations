using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Enums
{
    public enum OrderByEnum
    {
        [Description("By Date Ascending")] DateAscending = 1,
        [Description("By Date Descending")] DateDescending = 2,
        [Description("By Alphabetic Ascending")] AlphabeticAscending = 3,
        [Description("By Alphabetic Descending")] AlphabeticDescending = 4,
        [Description("RanKing")] RanKing = 5
    }
}