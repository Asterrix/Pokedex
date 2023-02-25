using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class ValidateCategory : AbstractValidator<Category>
{
    private const int CategoryNameMinLength = 3;
    private const int CategoryNameMaxLength = 12;

    public ValidateCategory()
    {
        CategoryNameValidator();
    }

    private void CategoryNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(CategoryNameMinLength)
            .MaximumLength(CategoryNameMaxLength);
    }
}