using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reservations.Business.Services.Contacts;
using Reservations.Business.Services.Reservations;
using Reservations.Core.Entities;
using Reservations.DataAccess.Contracts;
using Reservations.DataAccess.UnitOfWorkImpl;

namespace Reservations.Tests.Services
{
    [TestClass]
    public class ReservationServiceTest
    {
        private readonly IReservationService _reservationService;

        public ReservationServiceTest()
        {
            IUnitOfWork unitOfWork = new SqlUnitOfWork();
            _reservationService = new ReservationService(unitOfWork);
        }

        [TestMethod]
        public void Add()
        {
            var reservation = new Reservation()
            {
                ContactId = 1,
                RanKing = 4,
                Descriptions = "Other Test"
            };

            var newContact = this._reservationService.Add(reservation);
            Assert.IsTrue(newContact.Id != 0);
        }

        [TestMethod]
        public void UpdateRanKing()
        {
            var result = this._reservationService.UpdateRanKing(1, 5);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateFavorite()
        {
            var result = this._reservationService.UpdateFavorite(1, true);
            Assert.IsTrue(result);
        }
    }
}
