
using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Reservations.Business.Dto;
using Reservations.Core;
using Reservations.Core.Entities;
using Reservations.DataAccess.Contracts;

namespace Reservations.Business.Services.Contacts
{
   
    public class ContactService : IContactService
    {
       
        private readonly IRepository<Contact> repository;

      
        private readonly IUnitOfWork unitOfWork;

        //private readonly object Localization;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.Contacts;
        }


        public Contact Add(Contact input)
        {
            input.ContactType = null;
            this.repository.Add(input);
            this.unitOfWork.SaveChanges();
            return input;
        }


        public Contact Get(int id)
        {
            return this.repository.Get(id);
        }

        public Contact Get(string name)
        {
            return this.repository.Find(i => i.Name == name).FirstOrDefault();
        }


        public CollectionResponse<Contact> GetAll(PageResult input)
        {
            if (input.SkipCount < 0)
            {
                input.SkipCount = 0;
            }

            if (input.MaxCount < 0)
            {
                input.MaxCount = 0;
            }

            var entities = this.repository.GetAll();
            var result = entities.OrderBy(t => t.Name).Skip(input.SkipCount).Take(input.MaxCount).ToList();
            var response = new CollectionResponse<Contact> {SourceTotal = entities.Count()};
            response.Items.AddRange(result);
            return response;
        }

        public CollectionResponse<Contact> GetAll()
        {
            var entities = this.repository.GetAll();
            var result = entities.OrderBy(t => t.Name);
            var response = new CollectionResponse<Contact> { SourceTotal = entities.Count() };
            response.Items.AddRange(result);
            return response;
        }

        public Contact Update(Contact input)
        {
            var found = this.ValidateContactExists(input.Id);

            found.Name = input.Name;
            found.Birthdate = input.Birthdate;
            found.ContactTypeId = input.ContactTypeId;
            found.PhoneNumber = input.PhoneNumber;
            this.unitOfWork.SaveChanges();
            return null;
        }


        public void Delete(int id)
        {
            var found = this.ValidateContactExists(id);

            this.repository.Delete(found);
            this.unitOfWork.SaveChanges();
        }


        private Contact ValidateContactExists(int id)
        {
            var found = this.repository.Get(id);
            if (found != null)
            {
                return found;
            }

            var message = string.Format(Localization.ContactNotExists, id);
            throw new Exception(message);
        }


    }
}