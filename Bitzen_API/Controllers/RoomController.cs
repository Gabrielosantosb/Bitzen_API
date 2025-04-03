using Bitzen_API.Application.Services.Room;
using Bitzen_API.ORM.Model.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bitzen_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Cria uma nova sala.
        /// </summary>
        /// <param name="model">Dados da nova sala.</param>
        /// <returns>Retorna a sala criada ou erro.</returns>
        [HttpPost("create-room")]
        [Authorize]
        public IActionResult Create([FromBody] CreateRoomModel createRoomModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _roomService.CreateRoom(createRoomModel);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Atualiza os dados de uma sala.
        /// </summary>
        /// <param name="id">ID da sala.</param>
        /// <param name="model">Novos dados da sala.</param>
        /// <returns>Retorna a sala atualizada ou erro.</returns>
        [HttpPut("update-room/{roomId}")]
        [Authorize]
        public IActionResult Update(int roomId, [FromBody] UpdateRoomModel updateRoomModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _roomService.UpdateRoom(roomId, updateRoomModel);
            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Deleta uma sala.
        /// </summary>
        /// <param name="id">ID da sala.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpDelete("delete-room/{roomId}")]
        [Authorize]
        public IActionResult Delete(int roomId)
        {
            var result = _roomService.DeleteRoom(roomId);
            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Data });
        }

   
    }
}
