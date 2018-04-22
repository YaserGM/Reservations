using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reservations.Core.Entities;

namespace Reservations.App.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public double RanKing { get; set; }

      
        public DateTime CreateDate { get; set; }

      
        public string Descriptions { get; set; }

       
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

    }
}