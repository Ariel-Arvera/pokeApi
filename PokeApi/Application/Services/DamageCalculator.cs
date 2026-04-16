using PokeApi.Domain.Enums;

namespace PokeApi.Application.Services;

public class DamageCalculator
{
    private static readonly Dictionary<(PokemonType, PokemonType), double> EffectivenessTable = new()
    {
        // Fire -> others
        { (PokemonType.Fire, PokemonType.Grass), 2.0 },
        { (PokemonType.Fire, PokemonType.Ice), 2.0 },
        { (PokemonType.Fire, PokemonType.Bug), 2.0 },
        { (PokemonType.Fire, PokemonType.Steel), 2.0 },
        { (PokemonType.Fire, PokemonType.Fire), 0.5 },
        { (PokemonType.Fire, PokemonType.Water), 0.5 },
        { (PokemonType.Fire, PokemonType.Rock), 0.5 },
        { (PokemonType.Fire, PokemonType.Dragon), 0.5 },

        // Water -> others
        { (PokemonType.Water, PokemonType.Fire), 2.0 },
        { (PokemonType.Water, PokemonType.Ground), 2.0 },
        { (PokemonType.Water, PokemonType.Rock), 2.0 },
        { (PokemonType.Water, PokemonType.Water), 0.5 },
        { (PokemonType.Water, PokemonType.Grass), 0.5 },
        { (PokemonType.Water, PokemonType.Dragon), 0.5 },

        // Grass -> others
        { (PokemonType.Grass, PokemonType.Water), 2.0 },
        { (PokemonType.Grass, PokemonType.Ground), 2.0 },
        { (PokemonType.Grass, PokemonType.Rock), 2.0 },
        { (PokemonType.Grass, PokemonType.Fire), 0.5 },
        { (PokemonType.Grass, PokemonType.Grass), 0.5 },
        { (PokemonType.Grass, PokemonType.Poison), 0.5 },
        { (PokemonType.Grass, PokemonType.Flying), 0.5 },
        { (PokemonType.Grass, PokemonType.Bug), 0.5 },
        { (PokemonType.Grass, PokemonType.Dragon), 0.5 },

        // Electric -> others
        { (PokemonType.Electric, PokemonType.Water), 2.0 },
        { (PokemonType.Electric, PokemonType.Flying), 2.0 },
        { (PokemonType.Electric, PokemonType.Grass), 0.5 },
        { (PokemonType.Electric, PokemonType.Electric), 0.5 },
        { (PokemonType.Electric, PokemonType.Dragon), 0.5 },
        { (PokemonType.Electric, PokemonType.Ground), 0.0 },

        // Ice -> others
        { (PokemonType.Ice, PokemonType.Grass), 2.0 },
        { (PokemonType.Ice, PokemonType.Ground), 2.0 },
        { (PokemonType.Ice, PokemonType.Flying), 2.0 },
        { (PokemonType.Ice, PokemonType.Dragon), 2.0 },
        { (PokemonType.Ice, PokemonType.Fire), 0.5 },
        { (PokemonType.Ice, PokemonType.Water), 0.5 },
        { (PokemonType.Ice, PokemonType.Ice), 0.5 },
        { (PokemonType.Ice, PokemonType.Steel), 0.5 },

        // Fighting -> others
        { (PokemonType.Fighting, PokemonType.Normal), 2.0 },
        { (PokemonType.Fighting, PokemonType.Ice), 2.0 },
        { (PokemonType.Fighting, PokemonType.Rock), 2.0 },
        { (PokemonType.Fighting, PokemonType.Dark), 2.0 },
        { (PokemonType.Fighting, PokemonType.Steel), 2.0 },
        { (PokemonType.Fighting, PokemonType.Poison), 0.5 },
        { (PokemonType.Fighting, PokemonType.Flying), 0.5 },
        { (PokemonType.Fighting, PokemonType.Psychic), 0.5 },
        { (PokemonType.Fighting, PokemonType.Bug), 0.5 },
        { (PokemonType.Fighting, PokemonType.Fairy), 0.5 },
        { (PokemonType.Fighting, PokemonType.Ghost), 0.0 },

        // Poison -> others
        { (PokemonType.Poison, PokemonType.Grass), 2.0 },
        { (PokemonType.Poison, PokemonType.Fairy), 2.0 },
        { (PokemonType.Poison, PokemonType.Poison), 0.5 },
        { (PokemonType.Poison, PokemonType.Ground), 0.5 },
        { (PokemonType.Poison, PokemonType.Rock), 0.5 },
        { (PokemonType.Poison, PokemonType.Ghost), 0.5 },
        { (PokemonType.Poison, PokemonType.Steel), 0.0 },

        // Ground -> others
        { (PokemonType.Ground, PokemonType.Fire), 2.0 },
        { (PokemonType.Ground, PokemonType.Electric), 2.0 },
        { (PokemonType.Ground, PokemonType.Poison), 2.0 },
        { (PokemonType.Ground, PokemonType.Rock), 2.0 },
        { (PokemonType.Ground, PokemonType.Grass), 0.5 },
        { (PokemonType.Ground, PokemonType.Bug), 0.5 },
        { (PokemonType.Ground, PokemonType.Flying), 0.0 },

        // Flying -> others
        { (PokemonType.Flying, PokemonType.Grass), 2.0 },
        { (PokemonType.Flying, PokemonType.Fighting), 2.0 },
        { (PokemonType.Flying, PokemonType.Bug), 2.0 },
        { (PokemonType.Flying, PokemonType.Rock), 0.5 },
        { (PokemonType.Flying, PokemonType.Steel), 0.5 },
        { (PokemonType.Flying, PokemonType.Electric), 0.5 },

        // Psychic -> others
        { (PokemonType.Psychic, PokemonType.Fighting), 2.0 },
        { (PokemonType.Psychic, PokemonType.Poison), 2.0 },
        { (PokemonType.Psychic, PokemonType.Psychic), 0.5 },
        { (PokemonType.Psychic, PokemonType.Steel), 0.5 },
        { (PokemonType.Psychic, PokemonType.Dark), 0.0 },

        // Bug -> others
        { (PokemonType.Bug, PokemonType.Grass), 2.0 },
        { (PokemonType.Bug, PokemonType.Psychic), 2.0 },
        { (PokemonType.Bug, PokemonType.Dark), 2.0 },
        { (PokemonType.Bug, PokemonType.Fire), 0.5 },
        { (PokemonType.Bug, PokemonType.Fighting), 0.5 },
        { (PokemonType.Bug, PokemonType.Poison), 0.5 },
        { (PokemonType.Bug, PokemonType.Flying), 0.5 },
        { (PokemonType.Bug, PokemonType.Steel), 0.5 },
        { (PokemonType.Bug, PokemonType.Fairy), 0.5 },
        { (PokemonType.Bug, PokemonType.Ghost), 0.5 },

        // Rock -> others
        { (PokemonType.Rock, PokemonType.Fire), 2.0 },
        { (PokemonType.Rock, PokemonType.Ice), 2.0 },
        { (PokemonType.Rock, PokemonType.Bug), 2.0 },
        { (PokemonType.Rock, PokemonType.Flying), 2.0 },
        { (PokemonType.Rock, PokemonType.Fighting), 0.5 },
        { (PokemonType.Rock, PokemonType.Ground), 0.5 },
        { (PokemonType.Rock, PokemonType.Steel), 0.5 },

        // Ghost -> others
        { (PokemonType.Ghost, PokemonType.Psychic), 2.0 },
        { (PokemonType.Ghost, PokemonType.Ghost), 2.0 },
        { (PokemonType.Ghost, PokemonType.Normal), 0.0 },
        { (PokemonType.Ghost, PokemonType.Dark), 0.5 },

        // Dragon -> others
        { (PokemonType.Dragon, PokemonType.Dragon), 2.0 },
        { (PokemonType.Dragon, PokemonType.Steel), 0.5 },
        { (PokemonType.Dragon, PokemonType.Fairy), 0.0 },

        // Dark -> others
        { (PokemonType.Dark, PokemonType.Psychic), 2.0 },
        { (PokemonType.Dark, PokemonType.Ghost), 2.0 },
        { (PokemonType.Dark, PokemonType.Fighting), 0.5 },
        { (PokemonType.Dark, PokemonType.Dark), 0.5 },
        { (PokemonType.Dark, PokemonType.Fairy), 0.5 },

        // Steel -> others
        { (PokemonType.Steel, PokemonType.Ice), 2.0 },
        { (PokemonType.Steel, PokemonType.Rock), 2.0 },
        { (PokemonType.Steel, PokemonType.Fairy), 2.0 },
        { (PokemonType.Steel, PokemonType.Fire), 0.5 },
        { (PokemonType.Steel, PokemonType.Water), 0.5 },
        { (PokemonType.Steel, PokemonType.Electric), 0.5 },
        { (PokemonType.Steel, PokemonType.Steel), 0.5 },

        // Fairy -> others
        { (PokemonType.Fairy, PokemonType.Fighting), 2.0 },
        { (PokemonType.Fairy, PokemonType.Dragon), 2.0 },
        { (PokemonType.Fairy, PokemonType.Dark), 2.0 },
        { (PokemonType.Fairy, PokemonType.Fire), 0.5 },
        { (PokemonType.Fairy, PokemonType.Poison), 0.5 },
        { (PokemonType.Fairy, PokemonType.Steel), 0.5 },
    };

    public double CalculateDamage(int level, int power, int attack, int defense, PokemonType moveType, PokemonType defenderType)
    {
        double effectiveness = GetEffectiveness(moveType, defenderType);

        double randomFactor = new Random().Next(85, 101) / 100.0;

        double damage = ((2 * level / 5.0 + 2) * power * attack / (double)defense / 50.0 + 2) * effectiveness * randomFactor;

        return Math.Max(1, Math.Floor(damage));
    }

    public double GetEffectiveness(PokemonType attackType, PokemonType defenderType)
    {
        if (EffectivenessTable.TryGetValue((attackType, defenderType), out double effectiveness))
        {
            return effectiveness;
        }
        return 1.0;
    }
}