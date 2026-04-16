using PokeApi.Domain.Enums;
using PokeApi.Application.DTOs;

namespace PokeApi.Application.DTOs;

public class PokemonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public PokemonType Type { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpAttack { get; set; }
    public int SpDefense { get; set; }
    public int Speed { get; set; }
    public List<MoveDto> Moves { get; set; } = new();
}

public class CreatePokemonDto
{
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; } = 1;
    public PokemonType Type { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpAttack { get; set; }
    public int SpDefense { get; set; }
    public int Speed { get; set; }
    public List<int> MoveIds { get; set; } = new();
}