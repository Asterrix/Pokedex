using Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services.Category.Command;

public record PatchCategoryCommand(string CategoryName, JsonPatchDocument<Models.Category> PatchDocument) : IRequest<bool>;

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

        var category = await _categoryRepository.GetCategoryAsync(request.CategoryName.Trim(), cancellationToken);
        if (category is null)
        {
            throw new NotFoundException(
                $"Category with the name of \"{request.CategoryName.Trim()}\" could not be found.");
        }
        
        request.PatchDocument.ApplyTo(category);

        category.Name = category.Name.Trim();

        var checkNewValue = await _categoryRepository.GetCategoryAsync(category.Name, cancellationToken);
        if (checkNewValue is not null)
        {
            throw new InvalidOperationException(
                $"Nev value you are trying to assign to \"{request.CategoryName.Trim()}\" conflicts with existing category.");
        }

        #endregion

        await _validator.ValidateAndThrowAsync(category, cancellationToken);

        return await _categoryRepository.PatchCategoryAsync(category, cancellationToken);
    }
}