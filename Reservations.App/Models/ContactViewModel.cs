using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Reservations.Core;
using Reservations.Core.Entities;

namespace Reservations.App.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "ContactName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "PhoneNumberInvalid")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "Birthdate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "ContactType")]
        public int ContactTypeId { get; set; }

        [JsonIgnore]
        public ContactTypeViewModel ContactType { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}