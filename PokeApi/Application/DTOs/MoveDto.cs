using PokeApi.Domain.Enums;

namespace PokeApi.Application.DTOs;

public class MoveDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Power { get; set; }
    public PokemonType Type { get; set; }
}