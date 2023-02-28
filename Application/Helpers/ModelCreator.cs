using Application.Models;
using Application.Services.Category.Command;
using Application.Services.Generation.Command;
using Application.Services.Pokemon.Command;
using Application.Services.Specie.Command;

namespace Application.Helpers;

public static class ModelCreator
{
    public static Pokemon CreatePokemonModel(CreatePokemonCommand pokemon, Generation generation, Specie specie, IEnumerable<Category> categories)
    {
        return PokemonModelCreator.CreatePokemonModel(pokemon, generation, specie, categories);
    }

    public static Category CreateCategoryModel(CreateCategoryCommand command)
    {
        return CategoryModelCreator.CreateCategoryModel(command);
    }

    public static Generation CreateGenerationModel(CreateGenerationCommand command)
    {
        return GenerationModelCreator.CreateGenerationModel(command);
    }

    public static Specie CreateSpecieModel(CreateSpecieCommand command)
    {
        return SpecieModelCreator.CreateSpecieModel(command);
    }
}