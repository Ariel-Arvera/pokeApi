using FluentValidation;
using PokeApi.Application.DTOs;

namespace PokeApi.Application.Validators;

public class CreatePokemonValidator : AbstractValidator<CreatePokemonDto>
{
    public CreatePokemonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres")
            .MaximumLength(50).WithMessage("El nombre no puede exceder 50 caracteres");

        RuleFor(x => x.Level)
            .InclusiveBetween(1, 100).WithMessage("El nivel debe estar entre 1 y 100");

        RuleFor(x => x.HP)
            .GreaterThan(0).WithMessage("HP debe ser mayor a 0");

        RuleFor(x => x.MaxHP)
            .GreaterThan(0).WithMessage("MaxHP debe ser mayor a 0");

        RuleFor(x => x.Attack)
            .GreaterThan(0).WithMessage("Ataque debe ser mayor a 0");

        RuleFor(x => x.Defense)
            .GreaterThan(0).WithMessage("Defensa debe ser mayor a 0");

        RuleFor(x => x.SpAttack)
            .GreaterThan(0).WithMessage("Ataque especial debe ser mayor a 0");

        RuleFor(x => x.SpDefense)
            .GreaterThan(0).WithMessage("Defensa especial debe ser mayor a 0");

        RuleFor(x => x.Speed)
            .GreaterThan(0).WithMessage("Velocidad debe ser mayor a 0");

        RuleFor(x => x.MoveIds)
            .Must(ids => ids == null || ids.Count <= 4)
            .WithMessage("Un Pokémon solo puede tener máximo 4 movimientos");
    }
}