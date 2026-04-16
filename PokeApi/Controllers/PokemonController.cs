using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Application.DTOs;
using PokeApi.Application.Services;

namespace PokeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
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
    [HttpGet]
    [ProducesResponseType(typeof(List<PokemonDto>), 200)]
    public ActionResult<List<PokemonDto>> GetAll()
    {
        return Ok(_pokemonService.GetAll());
    }

    /// <summary>
    /// Obtiene un Pokémon por su ID.
    /// </summary>
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
    [HttpPost]
    [ProducesResponseType(typeof(PokemonDto), 201)]
    public ActionResult<PokemonDto> Create([FromBody] CreatePokemonDto createDto)
    {
        var pokemon = _pokemonService.Create(createDto);
        return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
    }

    /// <summary>
    /// Elimina un Pokémon por su ID.
    /// </summary>
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