using Application.Contracts;
using MediatR;

namespace Application.Services.Specie.Query;

public record GetSpecieListQuery() : IRequest<List<Models.Specie>>;

public class GetSpecieListQueryHandler : IRequestHandler<GetSpecieListQuery, List<Models.Specie>>
{
    private readonly ISpecieRepository _specieRepository;

    public GetSpecieListQueryHandler(ISpecieRepository specieRepository)
    {
        _specieRepository = specieRepository;
    }

    public async Task<List<Models.Specie>> Handle(GetSpecieListQuery request, CancellationToken cancellationToken)
    {
        var result = await _specieRepository.GetAllSpecieAsync(cancellationToken);
        return result;
    }
}