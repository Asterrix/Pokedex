using Application.Contracts;
using MediatR;

namespace Application.Services.Category.Command;

public record DeleteCategoryCommand(string Name) : IRequest<bool>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryAsync(request.Name.Trim(), cancellationToken);
        if (category == null)
        {
            throw new NotFoundException($"Category with the name of \"{request.Name.Trim()}\" could not be found.");
        }

        return await _categoryRepository.DeleteCategoryAsync(category, cancellationToken);
    }
}