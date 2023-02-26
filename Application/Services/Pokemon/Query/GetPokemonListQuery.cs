using Application.Contracts;
using Application.ViewModels;
using Application.ViewModels.Gender;
using Application.ViewModels.Pokemon;
using Application.ViewModels.Statistic;
using MediatR;

namespace Application.Services.Pokemon.Query;

public record GetPokemonListQuery : IRequest<List<PokemonGetViewModel>>;

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
        var result = await _pokemonRepository.GetAllPokemonAsync(cancellationToken);
        var mapResult = new List<PokemonGetViewModel>();

        foreach (var pokemon in result)
        {
            var model = new PokemonGetViewModel(
                pokemon.Id,
                pokemon.Name,
                pokemon.Portrait,
                pokemon.Height,
                pokemon.Weight,
                new GenderViewModel(pokemon.Gender.Male, pokemon.Gender.Female),
                pokemon.Description, 
                pokemon.Generation.Name, 
                pokemon.Specie.Name, 
                pokemon.Category.Name,
                new StatisticGetViewModel(
                    pokemon.Statistic.Hp,
                    pokemon.Statistic.Attack,
                    pokemon.Statistic.Defense,
                    pokemon.Statistic.SpecialAttack,
                    pokemon.Statistic.SpecialDefense,
                    pokemon.Statistic.Speed,
                    pokemon.Statistic.Total));
            mapResult.Add(model);
        }

        return mapResult;
    }
}