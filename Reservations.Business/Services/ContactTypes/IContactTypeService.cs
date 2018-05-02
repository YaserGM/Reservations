using Reservations.Business.Dto;
using Reservations.Core.Entities;

namespace Reservations.Business.Services.ContactTypes
{
    public interface IContactTypeService
    {
       
        ContactType Add(ContactType input);

        ContactType Get(int id);

        CollectionResponse<ContactType> GetAll();

        ContactType Update(ContactType input);

        void Delete(int id);
    }
}