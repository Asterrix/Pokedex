using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class ValidateStatistic : AbstractValidator<Statistic>
{
    private const int MinStatValue = 1;
    private const int MaxStatValue = 800;
    
    public ValidateStatistic()
    {
        PokemonStatisticValidator();
    }

    private void PokemonStatisticValidator()
    {
        RuleFor(x => x.Hp).InclusiveBetween(MinStatValue, MaxStatValue);
        RuleFor(x => x.Attack).InclusiveBetween(MinStatValue, MaxStatValue);
        RuleFor(x => x.Defense).InclusiveBetween(MinStatValue, MaxStatValue);
        RuleFor(x => x.SpecialAttack).InclusiveBetween(MinStatValue, MaxStatValue);
        RuleFor(x => x.SpecialDefense).InclusiveBetween(MinStatValue, MaxStatValue);
        RuleFor(x => x.Speed).InclusiveBetween(MinStatValue, MaxStatValue);
    }
}