using PokeApi.Application.Interfaces;
using PokeApi.Domain.Entities;

namespace PokeApi.Infrastructure.Repositories;

public class MoveRepository : IMoveRepository
{
    private readonly List<Move> _moves = new();

    public MoveRepository()
    {
        SeedMoves();
    }

    public List<Move> GetAll() => _moves;

    public Move? GetById(int id) => _moves.FirstOrDefault(m => m.Id == id);

    public void Add(Move move) => _moves.Add(move);

    public void AddRange(List<Move> moves) => _moves.AddRange(moves);

    private void SeedMoves()
    {
        _moves.AddRange(new List<Move>
        {
            new() { Id = 1, Name = "Ember", Power = 40, Type = Domain.Enums.PokemonType.Fire },
            new() { Id = 2, Name = "Flamethrower", Power = 90, Type = Domain.Enums.PokemonType.Fire },
            new() { Id = 3, Name = "Fire Blast", Power = 110, Type = Domain.Enums.PokemonType.Fire },
            new() { Id = 4, Name = "Water Gun", Power = 40, Type = Domain.Enums.PokemonType.Water },
            new() { Id = 5, Name = "Hydro Pump", Power = 110, Type = Domain.Enums.PokemonType.Water },
            new() { Id = 6, Name = "Surf", Power = 90, Type = Domain.Enums.PokemonType.Water },
            new() { Id = 7, Name = "Vine Whip", Power = 45, Type = Domain.Enums.PokemonType.Grass },
            new() { Id = 8, Name = "Solar Beam", Power = 120, Type = Domain.Enums.PokemonType.Grass },
            new() { Id = 9, Name = "Razor Leaf", Power = 55, Type = Domain.Enums.PokemonType.Grass },
            new() { Id = 10, Name = "Thunderbolt", Power = 90, Type = Domain.Enums.PokemonType.Electric },
            new() { Id = 11, Name = "Thunder", Power = 110, Type = Domain.Enums.PokemonType.Electric },
            new() { Id = 12, Name = "Quick Attack", Power = 40, Type = Domain.Enums.PokemonType.Normal },
            new() { Id = 13, Name = "Tackle", Power = 40, Type = Domain.Enums.PokemonType.Normal },
            new() { Id = 14, Name = "Scratch", Power = 40, Type = Domain.Enums.PokemonType.Normal },
            new() { Id = 15, Name = "Ice Beam", Power = 90, Type = Domain.Enums.PokemonType.Ice },
            new() { Id = 16, Name = "Blizzard", Power = 110, Type = Domain.Enums.PokemonType.Ice },
            new() { Id = 17, Name = "Psybeam", Power = 65, Type = Domain.Enums.PokemonType.Psychic },
            new() { Id = 18, Name = "Psychic", Power = 90, Type = Domain.Enums.PokemonType.Psychic },
            new() { Id = 19, Name = "Shadow Ball", Power = 80, Type = Domain.Enums.PokemonType.Ghost },
            new() { Id = 20, Name = "Dragon Claw", Power = 80, Type = Domain.Enums.PokemonType.Dragon }
        });
    }
}