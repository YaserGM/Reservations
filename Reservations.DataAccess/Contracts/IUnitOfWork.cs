
using System;
using Reservations.Core.Entities;

namespace Reservations.DataAccess.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
  
        IRepository<Contact> Contacts { get; }

        IRepository<Reservation> Reservations { get; }

        void SaveChanges();

    }
}