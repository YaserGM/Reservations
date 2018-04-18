
using System;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.Contracts
{

    /// <inheritdoc />
    /// <summary>
    ///     The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Public Properties

        /// <summary>
        ///     Gets the contacts.
        /// </summary>
        IRepository<Contact> Contacts { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The save changes.
        /// </summary>
        void SaveChanges();

        #endregion
    }
}