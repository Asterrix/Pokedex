using Application.Contracts;
using MediatR;

namespace Application.Services.Generation.Query;

public record GetGenerationListQuery : IRequest<List<Models.Generation>>;

public class GetGenerationListQueryHandler : IRequestHandler<GetGenerationListQuery, List<Models.Generation>>
{
    private readonly IGenerationRepository _generationRepository;

    public GetGenerationListQueryHandler(IGenerationRepository generationRepository)
    {
        _generationRepository = generationRepository;
    }

    public async Task<List<Models.Generation>> Handle(GetGenerationListQuery request, CancellationToken cancellationToken)
    {
        var result = await _generationRepository.GetAllGenerationsAsync(cancellationToken);
        return result;
    }
}