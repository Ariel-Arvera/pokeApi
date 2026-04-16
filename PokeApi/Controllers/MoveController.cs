using Microsoft.AspNetCore.Mvc;
using PokeApi.Application.DTOs;
using PokeApi.Application.Services;

namespace PokeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MoveController : ControllerBase
{
    private readonly MoveService _moveService;

    public MoveController(MoveService moveService)
    {
        _moveService = moveService;
    }

    /// <summary>
    /// Obtiene todos los movimientos disponibles.
    /// </summary>
    /// <returns>Lista de movimientos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<MoveDto>), 200)]
    public ActionResult<List<MoveDto>> GetAll()
    {
        return Ok(_moveService.GetAll());
    }

    /// <summary>
    /// Obtiene un movimiento por su ID.
    /// </summary>
    /// <param name="id">ID del movimiento</param>
    /// <returns>Datos del movimiento</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MoveDto), 200)]
    [ProducesResponseType(404)]
    public ActionResult<MoveDto> GetById(int id)
    {
        var move = _moveService.GetById(id);
        if (move == null)
            return NotFound(new { message = $"Movimiento con ID {id} no encontrado" });

        return Ok(move);
    }

    /// <summary>
    /// Crea un nuevo movimiento.
    /// </summary>
    /// <param name="moveDto">Datos del movimiento a crear</param>
    /// <returns>Movimiento creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MoveDto), 201)]
    [ProducesResponseType(400)]
    public ActionResult<MoveDto> Create([FromBody] MoveDto moveDto)
    {
        if (string.IsNullOrWhiteSpace(moveDto.Name))
            return BadRequest(new { message = "El nombre del movimiento es requerido" });

        if (moveDto.Power < 0 || moveDto.Power > 250)
            return BadRequest(new { message = "El poder debe estar entre 0 y 250" });

        var move = _moveService.Create(moveDto);
        return CreatedAtAction(nameof(GetById), new { id = move.Id }, move);
    }
}