using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Reservations.Core;
using Reservations.Core.Entities;

namespace Reservations.App.Models
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            this.Contact = new ContactViewModel() {ContactType = new ContactTypeViewModel()};
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof(Localization) , Name = "RanKing")]
        public double RanKing { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateDateLabel
        {
            get { return CreateDate.ToString("dddd MMMM d h:mm tt"); }}

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "Description")]
        public string Descriptions { get; set; }

        public bool Favorite { get; set; }

        public int ContactId { get; set; }

        public ContactViewModel Contact { get; set; }
    }
}