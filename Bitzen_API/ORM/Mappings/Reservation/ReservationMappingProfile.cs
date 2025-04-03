using AutoMapper;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Reservation;

namespace Bitzen_API.ORM.Mappings.Reservation
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<CreateReservationModel, ReservationModel>();            
        }
    }
}
