using Application.Contracts;
using Application.Mapper;
using Application.ViewModels.Pokemon;
using MediatR;

namespace Application.Services.Pokemon.Query;

public record GetPokemonQuery(string Name) : IRequest<PokemonGetViewModel?>;

public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, PokemonGetViewModel?>
{
    private readonly IPokemonRepository _pokemonRepository;

    public GetPokemonQueryHandler(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonGetViewModel?> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
    {
        var result = await _pokemonRepository.GetPokemonAsync(request.Name.Trim(), cancellationToken);
        if (result is null)
        {
            return null;
        }

        return PokemonMapper.ToPokemonGetViewModel(ref result);
    }
}