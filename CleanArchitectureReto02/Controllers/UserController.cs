using CleanArchitectureReto02.Data;
using CleanArchitectureReto02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureReto02.Controllers
{
    [ApiController]
    [Route("api/users")]
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// </summary>
    /// <remarks>
    /// Este controlador permite obtener la lista de usuarios y detalles de un usuario específico.
    /// </remarks>
    /// <response code="200">Retorna la lista de usuarios o el usuario específico.</response>
    /// <response code="404">Retorna un error si el usuario no es encontrado.</response>
    /// <response code="401">Retorna un error si el usuario no está
    public class UsersController : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [Produces("application/json")]  
        /// <summary>
        /// Obtiene la lista de todos los usuarios.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        /// <response code="200">Retorna la lista de usuarios.</response>
        /// <response code="401">Retorna un error si el usuario no está autorizado.</response>

        public IActionResult GetUsers()
        {
            return Ok(Users.GetUsers());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 401)]
        [Produces("application/json")]
        /// <summary>
        /// Obtiene los detalles de un usuario específico por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>Detalles del usuario.</returns>
        /// <response code="200">Retorna los detalles del usuario.</response>
        /// <response code="404">Retorna un error si el usuario no es encontrado.</response>
        /// <response code="401">Retorna un error si el usuario no está autorizado.</response>
        public IActionResult GetUserById(int id)
        {
            var user = Users.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
