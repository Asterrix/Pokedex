using Application.Models;
using Application.ViewModels.Category;
using Application.ViewModels.Gender;
using Application.ViewModels.Pokemon;
using Application.ViewModels.Statistic;

namespace Application.Mapper;

internal static class PokemonMapper
{
    internal static List<PokemonGetViewModel> ToPokemonGetViewModel(ref List<Pokemon> pokemons)
    {
        var list = new List<PokemonGetViewModel>();
        
        foreach (var pokemon in pokemons)
        {
            var p = pokemon;
            var categories = new List<CategoryRelationViewModel>();

            foreach (var categoryRelation in pokemon.Categories)
            {
                categories.Add(new CategoryRelationViewModel(new CategoryViewModel(categoryRelation.Category.Id, categoryRelation.Category.Name)));;
            }
            
            list.Add(ToPokemonGetViewModel(ref p, categories));
        }

        return list;
    }
    
    internal static PokemonGetViewModel ToPokemonGetViewModel(ref Pokemon pokemon)
    {
        // var categories = new CategoryRelationViewModel(new List<CategoryViewModel>());
        var categories = new List<CategoryRelationViewModel>();
        
        foreach (var categoryRelation in pokemon.Categories)
        {
            categories.Add(new CategoryRelationViewModel(new CategoryViewModel(categoryRelation.Category.Id, categoryRelation.Category.Name)));
        }

        return ToPokemonGetViewModel(ref pokemon, categories);
    }

    private static PokemonGetViewModel ToPokemonGetViewModel(ref Pokemon pokemon, List<CategoryRelationViewModel> categories)
    {
        var mapResult = new PokemonGetViewModel( 
            Id: pokemon.Id,
            Name: pokemon.Name,
            Portrait: pokemon.Portrait,
            Height: pokemon.Height,
            Weight: pokemon.Weight,
            Gender: new GenderViewModel(pokemon.Gender.Male, pokemon.Gender.Female),
            Description: pokemon.Description,
            Generation: pokemon.Generation.Name,
            Specie: pokemon.Specie.Name,
            Categories: categories,
            Statistics: new StatisticGetViewModel(
                Hp: pokemon.Statistics.Hp,
                Attack: pokemon.Statistics.Attack,
                Defense: pokemon.Statistics.Defense,
                SpecialAttack: pokemon.Statistics.Speed,
                SpecialDefense: pokemon.Statistics.SpecialDefense,
                Speed: pokemon.Statistics.Speed,
                Total: pokemon.Statistics.Total)
        );
        return mapResult;
    }
}