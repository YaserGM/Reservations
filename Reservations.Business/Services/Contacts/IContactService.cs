
using Reservations.Business.Dto;
using Reservations.Core.Entities;

namespace Reservations.Business.Services.Contacts
{
    public interface IContactService
    {
       
        Contact Add(Contact input);

        Contact Get(int id);

        Contact Get(string name);

        CollectionResponse<Contact> GetAll(PageResult input);

        CollectionResponse<Contact> GetAll();

        Contact Update(Contact input);

        void Delete(int id);
    }
}