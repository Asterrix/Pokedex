using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Services.Category.Command;

public record PatchCategoryCommand(string CategoryName, string NewName) : IRequest<bool>;

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

        var sameValue = String.Equals(request.CategoryName.Trim(), request.NewName.Trim());
        if (sameValue)
        {
            throw new InvalidOperationException("You are trying to update the category value with the same one.");
        }

        var category = await _categoryRepository.GetCategoryAsync(request.CategoryName.Trim(), cancellationToken);
        if (category is null)
        {
            throw new NotFoundException($"Category with the name of \"{request.CategoryName.Trim()}\" could not be found.");
        }

        var newCategory = await _categoryRepository.GetCategoryAsync(request.NewName.Trim(), cancellationToken);
        if (newCategory is not null && newCategory != category)
        {
            throw new InvalidOperationException(
                $"Nev value you are trying to assign to \"{request.CategoryName.Trim()}\" conflicts with existing category.");
        }

        #endregion

        category.Name = request.NewName.Trim();

        await _validator.ValidateAndThrowAsync(category, cancellationToken);

        return await _categoryRepository.PatchCategoryAsync(category, cancellationToken);
    }
}