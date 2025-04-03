using Bitzen_API.Application.Services.Reservation;
using Bitzen_API.ORM.Model.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bitzen_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        /// <summary>
        /// Retorna uma lista paginada de reservas, com filtros opcionais por sala, data e status.
        /// </summary>
        /// <param name="roomId">ID da sala .</param>
        /// <param name="date">Data da reserva .</param>
        /// <param name="status">Status da reserva: true (ativa) ou false (cancelada) .</param>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10).</param>
        /// <returns>Retorna uma lista paginada com as reservas filtradas.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetReservations(
        [FromQuery] int? roomId,
        [FromQuery] DateTime? date,
        [FromQuery] bool? status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = _reservationService.GetReservations(roomId, date, status, pageNumber, pageSize);
            return Ok(result);
        }


        /// <summary>
        /// Cria uma nova reserva de sala.
        /// </summary>      
        [HttpPost]
        //[Authorize]
        public IActionResult CreateReservation(int roomId, [FromBody] CreateReservationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _reservationService.CreateReservation(roomId, model);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Data });
        }
    }
}
