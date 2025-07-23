using CleanArchitectureReto02.Data;
using CleanArchitectureReto02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureReto02.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// Permite obtener la lista de usuarios y los detalles de un usuario específico.
    /// Utiliza la clase Users para acceder a los datos de los usuarios.
    /// Este controlador es parte de la arquitectura limpia del proyecto, separando las preocupaciones de gestión de usuarios.
    /// Requiere autenticación y autorización para acceder a los recursos.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Obtiene la lista de todos los usuarios.
        /// Requiere autorización con el rol de "Admin".    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [Produces("application/json")]  

        public IActionResult GetUsers()
        {
            return Ok(Users.GetUsers());
        }
        /// <summary>
        /// Obtiene los detalles de un usuario específico por su ID.
        /// Requiere autorización con el rol de "Admin" o "User".
        /// Si el usuario no existe, devuelve un error 404.
        /// Si el usuario no está autorizado, devuelve un error 401.
        /// El ID del usuario se pasa como parámetro en la URL.
        /// El método busca el usuario en la lista de usuarios y devuelve sus detalles si se encuentra.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 401)]
        [Produces("application/json")]
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
