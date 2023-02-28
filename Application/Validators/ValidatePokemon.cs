using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class PokemonValidator : AbstractValidator<Pokemon>
{
    /* Name */
    private const int PokemonNameMinLength = 3;
    private const int PokemonNameMaxLength = 15;

    /* Height */
    private const float PokemonHeightMin = 0.1f;
    private const float PokemonHeightMax = 20f;

    /* Weight */
    private const float PokemonWeightMin = 0.1f;
    private const float PokemonWeightMax = 999.9f;
    
    /* Description */
    private const int PokemonDescriptionMinLength = 100;
    private const int PokemonDescriptionMaxLength = 2000;

    public PokemonValidator()
    {
        PokemonNameValidator();
        PokemonHeightValidator();
        PokemonWeightValidator();
        PokemonDescriptionValidator();
        RuleFor(x => x.Statistics).SetValidator(new ValidateStatistic());
    }

    private void PokemonNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(PokemonNameMinLength)
            .MaximumLength(PokemonNameMaxLength);
    }

    private void PokemonHeightValidator()
    {
        RuleFor(x => x.Height)
            .NotEmpty()
            .InclusiveBetween(PokemonHeightMin, PokemonHeightMax);
    }

    private void PokemonWeightValidator()
    {
        RuleFor(x => x.Weight)
            .NotEmpty()
            .InclusiveBetween(PokemonWeightMin, PokemonWeightMax);
    }

    private void PokemonDescriptionValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(PokemonDescriptionMinLength)
            .MaximumLength(PokemonDescriptionMaxLength);
    }
}