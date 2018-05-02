using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reservations.Business.Services.Contacts;
using Reservations.Core.Entities;
using Reservations.DataAccess.Contracts;
using Reservations.DataAccess.UnitOfWorkImpl;

namespace Reservations.Tests.Services
{
    [TestClass]
    public class ContactServiceTest
    {
        private readonly IContactService _contactService;

        public ContactServiceTest()
        {
            IUnitOfWork unitOfWork = new SqlUnitOfWork();
            _contactService = new ContactService(unitOfWork);
        }

        [TestMethod]
        public void Add()
        {
            var contact = new Contact
            {
                Name = "Alex",
                Birthdate = DateTime.Now.Date,
                ContactTypeId = 2,
                PhoneNumber = "535-345-4963"
            };

            var newContact = this._contactService.Add(contact);
            Assert.IsTrue(newContact.Id != 0);
        }
    }
}
