using PokeApi.Application.DTOs;
using PokeApi.Application.Interfaces;
using PokeApi.Domain.Entities;

namespace PokeApi.Application.Services;

public class PokemonService
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMoveRepository _moveRepository;

    public PokemonService(IPokemonRepository pokemonRepository, IMoveRepository moveRepository)
    {
        _pokemonRepository = pokemonRepository;
        _moveRepository = moveRepository;
    }

    public List<PokemonDto> GetAll()
    {
        return _pokemonRepository.GetAll().Select(MapToDto).ToList();
    }

    public PokemonDto? GetById(int id)
    {
        var pokemon = _pokemonRepository.GetById(id);
        return pokemon != null ? MapToDto(pokemon) : null;
    }

    public PokemonDto Create(CreatePokemonDto createDto)
    {
        var moves = new List<Move>();
        
        if (createDto.MoveIds.Any())
        {
            var availableMoves = _moveRepository.GetAll();
            moves = availableMoves.Where(m => createDto.MoveIds.Contains(m.Id)).ToList();
        }

        var pokemon = new Pokemon
        {
            Id = GetNextId(),
            Name = createDto.Name,
            Level = createDto.Level,
            Type = createDto.Type,
            HP = createDto.HP,
            MaxHP = createDto.MaxHP,
            Attack = createDto.Attack,
            Defense = createDto.Defense,
            SpAttack = createDto.SpAttack,
            SpDefense = createDto.SpDefense,
            Speed = createDto.Speed,
            Moves = moves
        };

        _pokemonRepository.Add(pokemon);
        return MapToDto(pokemon);
    }

    public bool Delete(int id)
    {
        var existing = _pokemonRepository.GetById(id);
        if (existing == null) return false;

        _pokemonRepository.Delete(id);
        return true;
    }

    private int GetNextId()
    {
        var pokemons = _pokemonRepository.GetAll();
        return pokemons.Any() ? pokemons.Max(p => p.Id) + 1 : 1;
    }

    private static PokemonDto MapToDto(Pokemon pokemon)
    {
        return new PokemonDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level,
            Type = pokemon.Type,
            HP = pokemon.HP,
            MaxHP = pokemon.MaxHP,
            Attack = pokemon.Attack,
            Defense = pokemon.Defense,
            SpAttack = pokemon.SpAttack,
            SpDefense = pokemon.SpDefense,
            Speed = pokemon.Speed,
            Moves = pokemon.Moves.Select(m => new MoveDto
            {
                Id = m.Id,
                Name = m.Name,
                Power = m.Power,
                Type = m.Type
            }).ToList()
        };
    }
}