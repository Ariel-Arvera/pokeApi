using PokeApi.Application.DTOs;
using PokeApi.Domain.Enums;

namespace PokeApi.Application.Services;

public class BattleService
{
    private readonly DamageCalculator _damageCalculator;

    public BattleService(DamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public DamageResponseDto CalculateDamage(DamageRequestDto request)
    {
        var attacker = request.Attacker;
        var defender = request.Defender;
        var move = request.Move;

        int attackStat = move.Type.IsSpecialAttack() ? attacker.SpAttack : attacker.Attack;
        int defenseStat = move.Type.IsSpecialAttack() ? defender.SpDefense : defender.Defense;

        int damage = (int)_damageCalculator.CalculateDamage(
            attacker.Level,
            move.Power,
            attackStat,
            defenseStat,
            move.Type,
            defender.Type
        );

        double effectiveness = _damageCalculator.GetEffectiveness(move.Type, defender.Type);

        string message = GetEffectivenessMessage(effectiveness);

        return new DamageResponseDto
        {
            Damage = damage,
            Effectiveness = effectiveness,
            IsCritical = false,
            Message = message
        };
    }

    private string GetEffectivenessMessage(double effectiveness)
    {
        return effectiveness switch
        {
            0 => "¡No tiene efecto!",
            0.25 => "¡Es muy poco efectivo!",
            0.5 => "¡No es muy efectivo!",
            1.0 => "",
            2.0 => "¡Es muy efectivo!",
            4.0 => "¡Es extremadamente efectivo!",
            _ => ""
        };
    }
}

public static class PokemonTypeExtensions
{
    public static bool IsSpecialAttack(this PokemonType type)
    {
        return type is PokemonType.Fire or PokemonType.Water or PokemonType.Grass
            or PokemonType.Electric or PokemonType.Ice or PokemonType.Psychic
            or PokemonType.Dragon or PokemonType.Dark or PokemonType.Fairy;
    }
}