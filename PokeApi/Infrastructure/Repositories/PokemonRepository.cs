using PokeApi.Application.Interfaces;
using PokeApi.Domain.Entities;
using PokeApi.Domain.Enums;

namespace PokeApi.Infrastructure.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly List<Pokemon> _pokemons = new();
    private readonly IMoveRepository _moveRepository;

    public PokemonRepository(IMoveRepository moveRepository)
    {
        _moveRepository = moveRepository;
        SeedPokemons();
    }

    public List<Pokemon> GetAll() => _pokemons;

    public Pokemon? GetById(int id) => _pokemons.FirstOrDefault(p => p.Id == id);

    public void Add(Pokemon pokemon) => _pokemons.Add(pokemon);

    public void Update(Pokemon pokemon)
    {
        var index = _pokemons.FindIndex(p => p.Id == pokemon.Id);
        if (index >= 0) _pokemons[index] = pokemon;
    }

    public void Delete(int id) => _pokemons.RemoveAll(p => p.Id == id);

    public void AddRange(List<Pokemon> pokemons) => _pokemons.AddRange(pokemons);

    private void SeedPokemons()
    {
        var ember = _moveRepository.GetById(1);
        var scratch = _moveRepository.GetById(14);
        var tackle = _moveRepository.GetById(13);
        var vineWhip = _moveRepository.GetById(7);
        var waterGun = _moveRepository.GetById(4);
        var thunderbolt = _moveRepository.GetById(10);
        var quickAttack = _moveRepository.GetById(12);

        _pokemons.AddRange(new List<Pokemon>
        {
            new()
            {
                Id = 1,
                Name = "Charmander",
                Level = 5,
                Type = PokemonType.Fire,
                HP = 39,
                MaxHP = 39,
                Attack = 52,
                Defense = 43,
                SpAttack = 60,
                SpDefense = 50,
                Speed = 65,
                Moves = new List<Move> { ember!, scratch! }
            },
            new()
            {
                Id = 2,
                Name = "Squirtle",
                Level = 5,
                Type = PokemonType.Water,
                HP = 44,
                MaxHP = 44,
                Attack = 48,
                Defense = 65,
                SpAttack = 50,
                SpDefense = 64,
                Speed = 43,
                Moves = new List<Move> { waterGun!, tackle! }
            },
            new()
            {
                Id = 3,
                Name = "Bulbasaur",
                Level = 5,
                Type = PokemonType.Grass,
                HP = 45,
                MaxHP = 45,
                Attack = 49,
                Defense = 49,
                SpAttack = 65,
                SpDefense = 65,
                Speed = 45,
                Moves = new List<Move> { vineWhip!, tackle! }
            },
            new()
            {
                Id = 4,
                Name = "Pikachu",
                Level = 5,
                Type = PokemonType.Electric,
                HP = 35,
                MaxHP = 35,
                Attack = 55,
                Defense = 40,
                SpAttack = 50,
                SpDefense = 50,
                Speed = 90,
                Moves = new List<Move> { thunderbolt!, quickAttack! }
            },
            new()
            {
                Id = 5,
                Name = "Charizard",
                Level = 36,
                Type = PokemonType.Fire,
                HP = 156,
                MaxHP = 156,
                Attack = 104,
                Defense = 78,
                SpAttack = 159,
                SpDefense = 85,
                Speed = 100,
                Moves = new List<Move> { ember!, scratch! }
            }
        });
    }
}