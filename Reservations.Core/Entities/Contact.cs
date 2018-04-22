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
        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldVeryLarge")]
        [Display(ResourceType = typeof(Localization), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [PhoneAttribute(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "PhoneNumberInvalid")]
        [Display(ResourceType = typeof(Localization), Name = "PhoneNumber")]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:yyyy-MM-dd}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "ContactType")]
        public string ContactType { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}