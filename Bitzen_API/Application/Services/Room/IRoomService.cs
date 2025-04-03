using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Common;
using Bitzen_API.ORM.Model.Room;

namespace Bitzen_API.Application.Services.Room
{
    public interface IRoomService
    {
        Result<RoomModel> CreateRoom(CreateRoomModel createRoomModel);
        Result<RoomModel> UpdateRoom(int roomId, UpdateRoomModel updateRoomModel);
        Result<string> DeleteRoom(int roomId);        
    }
}
