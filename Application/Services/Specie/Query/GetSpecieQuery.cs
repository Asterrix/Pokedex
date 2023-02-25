using Application.Contracts;
using MediatR;

namespace Application.Services.Specie.Query;

public record GetSpecieQuery(string Name) : IRequest<Models.Specie?>;

public class GetSpecieQueryHandler : IRequestHandler<GetSpecieQuery, Models.Specie?>
{
    private readonly ISpecieRepository _specieRepository;

    public GetSpecieQueryHandler(ISpecieRepository specieRepository)
    {
        _specieRepository = specieRepository;
    }

    public async Task<Models.Specie?> Handle(GetSpecieQuery request, CancellationToken cancellationToken)
    {
        var result = await _specieRepository.GetSpecieAsync(request.Name.Trim());
        return result;
    }
}