using Bitzen_API.Application.Services.User;
using Bitzen_API.ORM.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace Bitzen_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Cria um novo Usuário com os dados fornecidos.
        /// </summary>
        /// <param name="createUserModel">Objeto contendo as informações do novo usuario.</param>
        /// <returns>Retorna o produto criado se bem-sucedido, ou 400 (Bad Request) em caso de erro.</returns>
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _userService.CreateUser(model);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

    }
}
