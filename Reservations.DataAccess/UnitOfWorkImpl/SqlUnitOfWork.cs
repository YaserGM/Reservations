using Reservations.Core.Entities;
using Reservations.DataAccess.Contracts;
using Reservations.DataAccess.DataContext;
using Reservations.DataAccess.RepositoryImpl;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

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

        private IRepository<ContactType> contactTypes;

        private IRepository<Reservation> reservations;

        #endregion

        #region Constructors and Desctructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlUnitOfWork" /> class.
        /// </summary>
        public SqlUnitOfWork()
        {
            var dataContext = new EfDbContext();
            var adapter = (IObjectContextAdapter) dataContext;
            this.Context = adapter.ObjectContext;
        }

        #endregion

        #region Private Properties

        /// <summary>
        ///     Gets the context.
        /// </summary>
        private ObjectContext Context { get; }


        public IRepository<Contact> Contacts =>
            this.contacts ?? (this.contacts = new SqlRepository<Contact>(this.Context));

        public IRepository<ContactType> ContactTypes =>
            this.contactTypes ?? (this.contactTypes = new SqlRepository<ContactType>(this.Context));

        public IRepository<Reservation> Reservations =>
            this.reservations ?? (this.reservations = new SqlRepository<Reservation>(this.Context));

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

        public int ExecuteStoreCommand(string commandText, params object[] parameters)
        {
            return this.Context.ExecuteStoreCommand(commandText, parameters);
        }

        #endregion
    }
}