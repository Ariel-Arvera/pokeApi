using Microsoft.AspNetCore.Mvc;
using PokeApi.Application.DTOs;
using PokeApi.Application.Services;

namespace PokeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    /// <summary>
    /// Obtiene todos los Pokémon disponibles.
    /// </summary>
    /// <returns>Lista de Pokémon</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<PokemonDto>), 200)]
    public ActionResult<List<PokemonDto>> GetAll()
    {
        return Ok(_pokemonService.GetAll());
    }

    /// <summary>
    /// Obtiene un Pokémon por su ID.
    /// </summary>
    /// <param name="id">ID del Pokémon</param>
    /// <returns>Datos del Pokémon</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PokemonDto), 200)]
    [ProducesResponseType(404)]
    public ActionResult<PokemonDto> GetById(int id)
    {
        var pokemon = _pokemonService.GetById(id);
        if (pokemon == null)
            return NotFound(new { message = $"Pokémon con ID {id} no encontrado" });

        return Ok(pokemon);
    }

    /// <summary>
    /// Crea un nuevo Pokémon.
    /// </summary>
    /// <param name="createDto">Datos del Pokémon a crear</param>
    /// <returns>Pokémon creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PokemonDto), 201)]
    [ProducesResponseType(400)]
    public ActionResult<PokemonDto> Create([FromBody] CreatePokemonDto createDto)
    {
        if (string.IsNullOrWhiteSpace(createDto.Name))
            return BadRequest(new { message = "El nombre del Pokémon es requerido" });

        if (createDto.Level < 1 || createDto.Level > 100)
            return BadRequest(new { message = "El nivel debe estar entre 1 y 100" });

        if (createDto.MoveIds.Count > 4)
            return BadRequest(new { message = "Un Pokémon solo puede tener máximo 4 movimientos" });

        var pokemon = _pokemonService.Create(createDto);
        return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
    }

    /// <summary>
    /// Elimina un Pokémon por su ID.
    /// </summary>
    /// <param name="id">ID del Pokémon a eliminar</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult Delete(int id)
    {
        var result = _pokemonService.Delete(id);
        if (!result)
            return NotFound(new { message = $"Pokémon con ID {id} no encontrado" });

        return NoContent();
    }
}