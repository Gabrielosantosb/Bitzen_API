using AutoMapper;
using Bitzen_API.Application.Services.Token;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Common;
using Bitzen_API.ORM.Model.Reservation;
using Bitzen_API.ORM.Repository;

namespace Bitzen_API.Application.Services.Reservation
{
    public class ReservationService : IReservationService
    {

        private readonly BaseRepository<ReservationModel> _reservationRepository;
        private readonly BaseRepository<RoomModel> _roomRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public ReservationService(BaseRepository<ReservationModel> reservationRepository, BaseRepository<RoomModel> roomRepository, ITokenService tokenService, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        public Result<string> CreateReservation(int roomId, CreateReservationModel model)
        {
            var existingRoom = _roomRepository.GetById(roomId);
            if(existingRoom == null) return Result<string>.Fail("ID da Room não existe!");

            if (model.StartTime.Date != model.EndTime.Date)
                return Result<string>.Fail("A reserva deve iniciar e finalizar no mesmo dia.");

            if (model.EndTime <= model.StartTime)
                return Result<string>.Fail("A data/hora final deve ser posterior à inicial.");

            if (HasConflict(roomId, model))
                return Result<string>.Fail("Já existe uma reserva ativa para a sala neste horário.");

            var reservation = _mapper.Map<ReservationModel>(model);
            reservation.Status = true;
            reservation.RoomId = roomId;           
            reservation.CreatedByUserId = _tokenService.GetUserId();

            _reservationRepository.Add(reservation);
            _reservationRepository.SaveChanges();

            return Result<string>.Ok("Reserva criada com sucesso.");
        }

        public Result<string> CancelReservation(int reservationId)
        {
            var reservation = _reservationRepository.GetById(reservationId);

            if (reservation == null)
                return Result<string>.Fail("Reserva não encontrada.");

            if (!reservation.Status)
                return Result<string>.Fail("A reserva já está cancelada.");

            reservation.Status = false;
            _reservationRepository.Update(reservation);
            _reservationRepository.SaveChanges();

            return Result<string>.Ok("Reserva cancelada com sucesso.");
        }



        public PagedResult<ReservationModel> GetReservations(int? roomId, DateTime? date, bool? status, int pageNumber = 1, int pageSize = 10)
        {
            var query = _reservationRepository.GetAll().AsQueryable();

            if (roomId is not null)
                query = query.Where(r => r.RoomId == roomId.Value);

            if (date is not null)
                query = query.Where(r => r.StartTime.Date == date.Value.Date);

            if (status is not null)
                query = query.Where(r => r.Status == status.Value);

            var totalCount = query.Count();

            var items = query
                .OrderBy(r => r.StartTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<ReservationModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        private bool HasConflict(int roomId, CreateReservationModel model)
        {
            return _reservationRepository.FindAll(r =>
                r.RoomId == roomId &&
                r.Status &&
                (
                    (model.StartTime >= r.StartTime && model.StartTime < r.EndTime) ||
                    (model.EndTime > r.StartTime && model.EndTime <= r.EndTime) ||
                    (model.StartTime <= r.StartTime && model.EndTime >= r.EndTime)
                )
            ).Any();
        }  
    }
}
