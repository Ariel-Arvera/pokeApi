using Microsoft.AspNetCore.Mvc;
using PokeApi.Application.DTOs;
using PokeApi.Application.Services;

namespace PokeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Inicia sesión y devuelve un token JWT
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(401)]
    public ActionResult<AuthResponseDto> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = _authService.Login(loginDto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Registra un nuevo usuario y devuelve un token JWT
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), 201)]
    [ProducesResponseType(400)]
    public ActionResult<AuthResponseDto> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var result = _authService.Register(registerDto);
            return CreatedAtAction(nameof(Login), new { username = result.Username }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}