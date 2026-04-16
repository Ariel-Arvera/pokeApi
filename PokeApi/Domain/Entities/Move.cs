using PokeApi.Domain.Enums;

namespace PokeApi.Domain.Entities;

public class Move
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Power { get; set; }
    public PokemonType Type { get; set; }
}