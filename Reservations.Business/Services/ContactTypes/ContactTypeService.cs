using System;
using System.Linq;
using Reservations.Business.Dto;
using Reservations.Core;
using Reservations.Core.Entities;
using Reservations.DataAccess.Contracts;

namespace Reservations.Business.Services.ContactTypes
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IRepository<ContactType> repository;


        private readonly IUnitOfWork unitOfWork;

        //private readonly object Localization;

        public ContactTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.ContactTypes;
        }


        public ContactType Add(ContactType input)
        {
            this.repository.Add(input);
            this.unitOfWork.SaveChanges();
            return input;
        }


        public ContactType Get(int id)
        {
            return this.repository.Get(id);
        }

        public CollectionResponse<ContactType> GetAll()
        {
            var entities = this.repository.GetAll();
            var result = entities.OrderByDescending(t => t.Description);
            var response = new CollectionResponse<ContactType> {SourceTotal = entities.Count()};
            response.Items.AddRange(result);
            return response;
        }

        public ContactType Update(ContactType input)
        {
            var found = this.ValidateContactTypetExists(input.Id);

            found.Description = input.Description;
            this.unitOfWork.SaveChanges();
            return null;
        }


        public void Delete(int id)
        {
            var found = this.ValidateContactTypetExists(id);
            this.repository.Delete(found);
            this.unitOfWork.SaveChanges();
        }


        private ContactType ValidateContactTypetExists(int id)
        {
            var found = this.repository.Get(id);
            if (found != null)
            {
                return found;
            }

            var message = string.Format(Localization.ContactTypeNotExists, id);
            throw new Exception(message);
        }
    }
}