using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Common;
using Bitzen_API.ORM.Model.Reservation;

namespace Bitzen_API.Application.Services.Reservation
{
    public interface IReservationService
    {
        Result<string> CreateReservation(int roomId, CreateReservationModel model);
        Result<string> CancelReservation(int reservationId);

        PagedResult<ReservationModel> GetReservations(
            int? roomId,
            DateTime? date,
            bool? status,
            int pageNumber = 1,
            int pageSize = 10
        );
    }
}
