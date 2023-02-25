using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Services.Category.Command;

public record PatchCategoryCommand(string Name, string NewValue) : IRequest<bool>;

public class PatchCategoryCommandHandler : IRequestHandler<PatchCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<Models.Category> _validator;

    public PatchCategoryCommandHandler(ICategoryRepository categoryRepository, IValidator<Models.Category> validator)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
    }

    public async Task<bool> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
    {
        #region Validation

        var sameValue = String.Equals(request.Name.Trim(), request.NewValue.Trim(), StringComparison.OrdinalIgnoreCase);
        if (sameValue)
        {
            throw new InvalidOperationException("Category name cannot be the same as the old one.");
        }

        var category = await _categoryRepository.GetCategoryAsync(request.Name.Trim());
        if (category is null)
        {
            throw new NotFoundException($"Category with the name of \"{request.Name.Trim()}\" could not be found.");
        }

        var newCategory = await _categoryRepository.GetCategoryAsync(request.NewValue.Trim());
        if (newCategory is not null)
        {
            throw new InvalidOperationException(
                $"Nev value you are trying to assign to \"{request.Name.Trim()}\" conflicts with existing category.");
        }

        #endregion

        var entity = new Models.Category
        {
            Name = request.NewValue.Trim()
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        return await _categoryRepository.PatchCategoryAsync(category, request.NewValue.Trim());
    }
}