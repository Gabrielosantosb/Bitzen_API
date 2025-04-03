using AutoMapper;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Room;

namespace Bitzen_API.ORM.Mappings.Room
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<CreateRoomModel, RoomModel>();
            CreateMap<UpdateRoomModel, RoomModel>()
                .ForMember(dest => dest.RoomId, opt => opt.Ignore());
        }
    }
}
