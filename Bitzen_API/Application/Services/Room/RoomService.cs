using AutoMapper;
using Bitzen_API.Application.Services.Token;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Common;
using Bitzen_API.ORM.Model.Room;
using Bitzen_API.ORM.Repository;

namespace Bitzen_API.Application.Services.Room
{
    public class RoomService : IRoomService

    {
        private readonly BaseRepository<RoomModel> _roomRepository;        
        private readonly BaseRepository<UserModel> _userRepository;        
        private readonly IMapper _mapper;
        private ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public RoomService(BaseRepository<RoomModel> roomRepository, BaseRepository<UserModel> userRepository, IMapper mapper, IConfiguration configuration, ITokenService tokenService)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public Result<RoomResponseModel> CreateRoom(CreateRoomModel createRoomModel)
        {            
            var newRoom = _mapper.Map<RoomModel>(createRoomModel);
            
            var userId = _tokenService.GetUserId();
            
            newRoom.CreatedByUserId = userId;
            
            var result = _roomRepository.Add(newRoom);
            _roomRepository.SaveChanges();
            
            var mapped = _mapper.Map<RoomResponseModel>(result);
            return Result<RoomResponseModel>.Ok(mapped);
        }


        public Result<RoomModel> UpdateRoom(int roomId, UpdateRoomModel updateRoomModel)
        {
            var room = _roomRepository.GetById(roomId);
            if (room == null)
                return Result<RoomModel>.Fail("Sala não encontrada.");

            _mapper.Map(updateRoomModel, room);
            _roomRepository.Update(room);
            _roomRepository.SaveChanges();
            return Result<RoomModel>.Ok(room);
        }

        public Result<string> DeleteRoom(int roomId)
        {
            var room = _roomRepository.GetById(roomId);
            if (room == null)
                return Result<string>.Fail("Sala não achada.");

            _roomRepository.Delete(room);
            _roomRepository.SaveChanges();
            return Result<string>.Ok("Sala excluída com sucesso.");
        }

     
    }
}
