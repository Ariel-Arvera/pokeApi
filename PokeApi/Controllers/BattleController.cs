using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Application.DTOs;
using PokeApi.Application.Services;

namespace PokeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class BattleController : ControllerBase
{
    private readonly BattleService _battleService;

    public BattleController(BattleService battleService)
    {
        _battleService = battleService;
    }

    /// <summary>
    /// Calcula el daño que un Pokémon inflige a otro usando un movimiento específico.
    /// </summary>
    [HttpPost("calculate-damage")]
    [ProducesResponseType(typeof(DamageResponseDto), 200)]
    [ProducesResponseType(400)]
    public ActionResult<DamageResponseDto> CalculateDamage([FromBody] DamageRequestDto request)
    {
        if (request.Attacker == null)
            return BadRequest(new { message = "Los datos del Pokémon atacante son requeridos" });

        if (request.Defender == null)
            return BadRequest(new { message = "Los datos del Pokémon defensor son requeridos" });

        if (request.Move == null)
            return BadRequest(new { message = "Los datos del movimiento son requeridos" });

        if (string.IsNullOrWhiteSpace(request.Attacker.Name))
            return BadRequest(new { message = "El nombre del atacante es requerido" });

        if (string.IsNullOrWhiteSpace(request.Defender.Name))
            return BadRequest(new { message = "El nombre del defensor es requerido" });

        if (string.IsNullOrWhiteSpace(request.Move.Name))
            return BadRequest(new { message = "El nombre del movimiento es requerido" });

        var result = _battleService.CalculateDamage(request);
        return Ok(result);
    }
}