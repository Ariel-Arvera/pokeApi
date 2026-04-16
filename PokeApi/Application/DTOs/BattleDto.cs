using PokeApi.Domain.Enums;
using PokeApi.Application.DTOs;

namespace PokeApi.Application.DTOs;

public class DamageRequestDto
{
    public PokemonDto Attacker { get; set; } = null!;
    public PokemonDto Defender { get; set; } = null!;
    public MoveDto Move { get; set; } = null!;
}

public class DamageResponseDto
{
    public int Damage { get; set; }
    public bool IsCritical { get; set; }
    public double Effectiveness { get; set; }
    public string Message { get; set; } = string.Empty;
}