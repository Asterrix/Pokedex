using Application.Models;
using Application.Services.Pokemon.Command;

namespace Application.Helpers;

internal static class PokemonModelCreator
{
    internal static Pokemon CreatePokemonModel(CreatePokemonCommand pokemon, Generation generation, Specie specie, IEnumerable<Category> categories)
    {
        var entity = new Pokemon
        {
            Name = pokemon.Name.Trim(),
            Portrait = pokemon.Portrait.Trim(),
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            Gender = new Gender
            {
                Male = pokemon.Gender.Male,
                Female = pokemon.Gender.Female
            },
            Description = pokemon.Description.Trim(),
            Generation = generation,
            Specie = specie,
            Categories = CreateCategoryRelations(categories),
            Statistics = new Statistic
            {
                Hp = pokemon.Statistics.Hp,
                Attack = pokemon.Statistics.Attack,
                Defense = pokemon.Statistics.Defense,
                SpecialAttack = pokemon.Statistics.SpecialAttack,
                SpecialDefense = pokemon.Statistics.SpecialDefense,
                Speed = pokemon.Statistics.Speed
            }
        };

        return entity;
    }

    private static List<CategoryRelation> CreateCategoryRelations(IEnumerable<Category> categories)
    {
        return categories.Select(t => new CategoryRelation { Category = t }).ToList();
    }
}