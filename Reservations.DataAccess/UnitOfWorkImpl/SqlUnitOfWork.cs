
using Reservations.Core.Entities;
using Reservations.DataAccess.Contracts;
using Reservations.DataAccess.DataContext;
using Reservations.DataAccess.RepositoryImpl;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Reservations.DataAccess.UnitOfWorkImpl
{

    /// <inheritdoc />
    /// <summary>
    ///     The sql unit of work.
    /// </summary>
    public class SqlUnitOfWork : IUnitOfWork
    {
        #region Fields

        /// <summary>
        ///     The contacts.
        /// </summary>
        private IRepository<Contact> contacts;

        #endregion

        #region Constructors and Desctructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlUnitOfWork" /> class.
        /// </summary>
        public SqlUnitOfWork()
        {
            var dataContext = new EfDbContext();
            var adapter = (IObjectContextAdapter)dataContext;
            this.Context = adapter.ObjectContext;
        }

        #endregion

        #region Private Properties

        /// <summary>
        ///     Gets the context.
        /// </summary>
        private ObjectContext Context { get; }

        #endregion

        #region IUnitOfWork Members

        /// <inheritdoc />
        /// <summary>
        ///     The contacts.
        /// </summary>
        public IRepository<Contact> Contacts =>
            this.contacts ?? (this.contacts = new SqlRepository<Contact>(this.Context));

        /// <inheritdoc />
        /// <summary>
        /// The save changes.
        /// </summary>
        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        /// <inheritdoc />
        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Context?.Dispose();
        }

        #endregion
    }
}