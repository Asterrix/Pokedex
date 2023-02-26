using Application.Contracts;
using MediatR;

namespace Application.Services.Category.Query;

public record GetCategoryQuery(string Name) : IRequest<Models.Category?>;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Models.Category?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Models.Category?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetCategoryAsync(request.Name.Trim(), cancellationToken);
        return result;
    }
}