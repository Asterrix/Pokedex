using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class ValidateSpecie : AbstractValidator<Specie>
{
    private const int SpecieNameMinLength = 3;
    private const int SpecieNameMaxLength = 12;

    public ValidateSpecie()
    {
        SpecieNameValidator();
    }

    private void SpecieNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(SpecieNameMinLength)
            .MaximumLength(SpecieNameMaxLength);
    }
}