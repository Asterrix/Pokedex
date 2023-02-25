using Application.Contracts;
using MediatR;

namespace Application.Services.Category.Query;

public record GetCategoryListQuery() : IRequest<List<Models.Category>>;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<Models.Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryListQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Models.Category>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetAllCategoryAsync();
        return result;
    }
}