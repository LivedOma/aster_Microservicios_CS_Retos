using CleanArchitectureReto02.Models;
using CleanArchitectureReto02.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureReto02.Data; 

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Controlador para autenticación de usuarios.
/// </summary>
/// <remarks>/// Este controlador maneja el inicio de sesión de los usuarios y la generación de tokens JWT.
/// </remarks>
/// <response code="200">Retorna el token JWT y la fecha de expiración.</response>
/// <response code="400">Retorna un error si el usuario o la contraseña son inválidos.</response>
/// <response code="401">Retorna un error si las credenciales son incorrect
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 401)]
    /// <summary>
    /// Inicia sesión y genera un token JWT.
    /// </summary>
    /// <param name="request">Objeto que contiene el nombre de usuario y la contraseña.</param>
    /// <returns>Un objeto que contiene el token JWT y la fecha de expiración.</returns>
    /// <response code="200">Retorna el token JWT y la fecha de expiración.</response>
    /// <response code="400">Retorna un error si el usuario o la contraseña son inválidos.</response>
    /// <response code="401">Retorna un error si las credenciales son incorrectas.</response>
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