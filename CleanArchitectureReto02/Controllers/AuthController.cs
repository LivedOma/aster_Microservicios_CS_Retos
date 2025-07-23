using CleanArchitectureReto02.Models;
using CleanArchitectureReto02.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureReto02.Data; 

/// <summary>
/// Controlador para la autenticación de usuarios.
/// Permite iniciar sesión y generar un token JWT para el acceso a recursos protegidos.
/// Utiliza un servicio de JWT para la generación del token.
/// El token incluye el nombre de usuario y el rol del usuario autenticado.
/// El token tiene una duración de 30 minutos y se puede extender según la configuración del servicio.
/// Este controlador es parte de la arquitectura limpia del proyecto, separando las preocupaciones de autenticación.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    /// <summary>
    /// Inicia sesión y genera un token JWT.
    /// </summary>
    /// <param name="request">Objeto que contiene el nombre de usuario y la contraseña.</param>
    /// <returns>Un objeto que contiene el token JWT y la fecha de expiración.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 401)]
    [Produces("application/json")]  
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { message = "Usuario y contraseña son requeridos" });
        }

        var user = Users.GetUsers().FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
        if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Role))
        {
            return Unauthorized(new { message = "Credenciales inválidas" });
        }

        var token = _jwtService.GenerateToken(user.Username, user.Role);

        return Ok(new LoginResponse
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(30) // Ajustar según la configuración del token
        });
    }
}