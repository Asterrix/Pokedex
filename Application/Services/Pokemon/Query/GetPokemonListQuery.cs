using Application.Contracts;
using Application.Mapper;
using Application.ViewModels.Pokemon;
using MediatR;

namespace Application.Services.Pokemon.Query;

public record GetPokemonListQuery(string Name) : IRequest<List<PokemonGetViewModel>>;

public class GetPokemonListQueryHandler : IRequestHandler<GetPokemonListQuery, List<PokemonGetViewModel>>
{
    private readonly IPokemonRepository _pokemonRepository;

    public GetPokemonListQueryHandler(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<List<PokemonGetViewModel>> Handle(GetPokemonListQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Name == "")
        {
            var defaultList = await _pokemonRepository.GetAllPokemonAsync(cancellationToken);
            return PokemonMapper.ToPokemonGetViewModel(ref defaultList);
        }
        
        var result = await _pokemonRepository.GetAllPokemonAsync(cancellationToken, request.Name.Trim());

        return PokemonMapper.ToPokemonGetViewModel(ref result);
    }
}