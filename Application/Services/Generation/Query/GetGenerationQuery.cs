using Application.Contracts;
using MediatR;

namespace Application.Services.Generation.Query;

public record GetGenerationQuery(string Generation) : IRequest<Models.Generation?>;

public class GetGenerationQueryHandler : IRequestHandler<GetGenerationQuery, Models.Generation?>
{
    private readonly IGenerationRepository _generationRepository;

    public GetGenerationQueryHandler(IGenerationRepository generationRepository)
    {
        _generationRepository = generationRepository;
    }

    public async Task<Models.Generation?> Handle(GetGenerationQuery request, CancellationToken cancellationToken)
    {
        var result = await _generationRepository.GetGenerationAsync(request.Generation.ToLower().Trim());

        return result;
    }
}