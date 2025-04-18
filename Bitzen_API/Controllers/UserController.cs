﻿using Bitzen_API.Application.Services.Token;
using Bitzen_API.Application.Services.User;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.User;
using Bitzen_API.ORM.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        /// Autentica o usuário e retorna um token JWT em caso de sucesso.
        /// </summary>
        /// <param name="model">Credenciais de login.</param>
        /// <returns>Token JWT e dados do usuário autenticado.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestLoginModel model)
        {
            var result = await _userService.AuthenticateAsync(model.Email, model.Password);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(result.Data);
        }



        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="model">Dados do usuário a ser criado.</param>
        /// <returns>Retorna o usuário criado ou mensagem de erro.</returns>
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] CreateUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _userService.CreateUser(model);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="userId">ID do usuário a ser atualizado.</param>
        /// <param name="model">Novos dados do usuário.</param>
        /// <returns>Retorna o usuário atualizado ou mensagem de erro.</returns>
        [HttpPut("update-user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.UpdateUser(userId, model);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        /// <param name="userId">ID do usuário a ser deletado.</param>
        /// <returns>Retorna mensagem de sucesso ou erro.</returns>
        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var result = _userService.DeleteUser(userId);
            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Data });
        }
    }
}
