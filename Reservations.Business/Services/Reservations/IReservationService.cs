using Reservations.Business.Dto;
using Reservations.Core.Entities;
using Reservations.Core.Enums;

namespace Reservations.Business.Services.Reservations
{
    public interface IReservationService
    {
        Reservation Add(Reservation input);

        Reservation Get(int id);

        CollectionResponse<Reservation> GetAll(PageResult input);

        CollectionResponse<Reservation> GetAll();

        CollectionResponse<Reservation> OrderBy(OrderByEnum order, PageResult input = null);

        Reservation Update(Reservation input);

        void Delete(int id);
    }
}