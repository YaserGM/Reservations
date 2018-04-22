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
      
        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [Display(ResourceType = typeof(Localization), Name = "RanKing")]
        public double RanKing { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dddd, MMMM d, yyyy}")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = "FieldRequired")]
        public string Descriptions { get; set; }

        [Required]
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }

    }
}