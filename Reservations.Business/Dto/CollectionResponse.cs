// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionResponse.cs" company="ISUCorp">
//   ISUCorp 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Reservations.Business.Dto
{
    #region NameSpaces

    #endregion

    /// <summary>
    /// The collection response.
    /// </summary>
    /// <typeparam name="T">
    /// The response items type.
    /// </typeparam>
    public class CollectionResponse<T>
    {
        #region Constructors and Desctructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionResponse{T}"/> class.
        /// </summary>
        public CollectionResponse()
        {
            this.Items = new List<T>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the source total.
        /// </summary>
        public int SourceTotal { get; set; }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        public List<T> Items { get; }

        #endregion
    }
}