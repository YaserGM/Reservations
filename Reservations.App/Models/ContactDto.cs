using System;

namespace Reservations.App.Models
{
    #region NameSpaces

    #endregion

    /// <summary>
    ///     The contact dto.
    /// </summary>
    public class ContactDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the phone.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets the birthdate.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        ///     Gets or sets the contact type id.
        /// </summary>
        public string ContactType { get; set; }

        public string BirthdateText { get; set; }

        public string BirthdateString()
        {
            return Birthdate.ToString("yyyy-MM-dd");
        }

        #endregion
    }
}