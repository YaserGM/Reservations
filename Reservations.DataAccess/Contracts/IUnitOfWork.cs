using System;
using System.Data.Entity.Core.Objects;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Contact> Contacts { get; }

        IRepository<ContactType> ContactTypes { get; }

        IRepository<Reservation> Reservations { get; }

        void SaveChanges();

        int ExecuteStoreCommand(string commandText, params object[] parameters);
    }
}