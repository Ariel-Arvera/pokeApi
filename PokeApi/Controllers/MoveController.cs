using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Application.DTOs;
using PokeApi.Application.Services;

namespace PokeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
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
    [HttpGet]
    [ProducesResponseType(typeof(List<MoveDto>), 200)]
    public ActionResult<List<MoveDto>> GetAll()
    {
        return Ok(_moveService.GetAll());
    }

    /// <summary>
    /// Obtiene un movimiento por su ID.
    /// </summary>
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
    [HttpPost]
    [ProducesResponseType(typeof(MoveDto), 201)]
    public ActionResult<MoveDto> Create([FromBody] MoveDto moveDto)
    {
        var move = _moveService.Create(moveDto);
        return CreatedAtAction(nameof(GetById), new { id = move.Id }, move);
    }
}