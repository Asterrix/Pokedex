using Application.Contracts;
using MediatR;

namespace Application.Services.Pokemon.Query;

public record GetPokemonQuery(string Name) : IRequest<Models.Pokemon?>;

public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, Models.Pokemon?>
{
    private readonly IPokemonRepository _pokemonRepository;

    public GetPokemonQueryHandler(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<Models.Pokemon?> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
    {
        var result = await _pokemonRepository.GetPokemonAsync(request.Name.Trim(), cancellationToken);
        return result;
    }
}