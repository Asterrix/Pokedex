using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class ValidateGeneration : AbstractValidator<Generation>
{
    private const int GenerationNameMinLength = 12;
    private const int GenerationNameMaxLength = 15;

    public ValidateGeneration()
    {
        GenerationNameValidator();
    }

    private void GenerationNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(GenerationNameMinLength)
            .MaximumLength(GenerationNameMaxLength);
    }
}