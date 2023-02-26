using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Services.Category.Command;

public record CreateCategoryCommand(string Name) : IRequest<Models.Category>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Models.Category>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<Models.Category> _validator;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IValidator<Models.Category> validator)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
    }

    public async Task<Models.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryAsync(request.Name.Trim(), cancellationToken);
        if (category is not null)
        {
            throw new InvalidOperationException($"Category with the name of \"{request.Name.Trim()}\" already exists.");
        }

        var entity = new Models.Category
        {
            Name = request.Name.Trim()
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        return await _categoryRepository.CreateCategoryAsync(entity, cancellationToken);
    }
}