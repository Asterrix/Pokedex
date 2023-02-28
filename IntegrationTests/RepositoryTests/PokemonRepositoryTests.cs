using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using Application.Models;
using Infrastructure.Persistence.Repositories;
using IntegrationTests.RepositoryTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.RepositoryTests;

public class PokemonRepositoryTests
{
    [Fact]
    public async Task PokemonRepositoryGetAllPokemonAsyncShouldReturnEmptyListPokemon()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new PokemonRepository(context);

        #endregion

        #region Act

        var result = await repository.GetAllPokemonAsync(new CancellationToken());

        #endregion

        #region Assert

        Assert.IsType<List<Pokemon>>(result);
        Assert.Empty(result);

        #endregion
    }

    [Fact]
    public async Task PokemonRepositoryGetPokemonAsyncShouldReturnNull()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new PokemonRepository(context);

        #endregion

        #region Act

        var result = await repository.GetPokemonAsync("Null", new CancellationToken());

        #endregion

        #region Assert

        Assert.Null(result);
        Assert.IsNotType<Pokemon>(result);

        #endregion
    }

    [Fact]
    public async Task PokemonRepositoryGetPokemonAsyncShouldReturnOneItem()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new PokemonRepository(context);

        var expected = new Pokemon
        {
            Name = "Pikachu",
            Portrait = "Portrait",
            Height = 2,
            Weight = 5,
            Gender = new Gender() { Male = true, Female = false },
            Description = "Description",
            Generation = new Generation() { Name = "Any" },
            Specie = new Specie() { Name = "Any" },
            Categories = new List<CategoryRelation>()
            {
                new CategoryRelation()
                {
                    Category = new Category()
                    {
                        Name = "Any"
                    }
                }
            },
            Statistics = new Statistic
            {
                Hp = 100,
                Attack = 100,
                Defense = 100,
                SpecialAttack = 100,
                SpecialDefense = 100,
                Speed = 100,
                Total = 600
            }
        };

        await context.Pokemons.AddAsync(expected);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.GetPokemonAsync("pikachu", new CancellationToken());

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.IsType<Pokemon>(result);
        Assert.Equal(expected, result);

        #endregion
    }

    [Fact]
    public async Task CreatePokemonAsyncShouldCreateAndStorePokemonInsideTheDatabaseAndReturnCreatedPokemon()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new PokemonRepository(context);

        var model = new Pokemon
        {
            Name = "Pikachu",
            Portrait = "Portrait",
            Height = 2,
            Weight = 5,
            Gender = new Gender() { Male = true, Female = false },
            Description = "Description",
            Generation = new Generation() { Name = "Any" },
            Specie = new Specie() { Name = "Any" },
            Categories = new List<CategoryRelation>()
            {
                new CategoryRelation()
                {
                    Category = new Category()
                    {
                        Name = "Any"
                    }
                }
            },
            Statistics = new Statistic
            {
                Hp = 100,
                Attack = 100,
                Defense = 100,
                SpecialAttack = 100,
                SpecialDefense = 100,
                Speed = 100,
                Total = 600
            }
        };

        #endregion

        #region Act

        var result = await repository.CreatePokemonAsync(model, new CancellationToken());

        var storedValue = await context.Pokemons.FirstOrDefaultAsync(x => x.Name == result.Name);

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.NotNull(storedValue);
        Assert.IsType<Pokemon>(result);
        Assert.Equal(model, result);
        Assert.Equal(result, storedValue);

        #endregion
    }

    [Fact]
    public async Task PatchPokemonAsyncShouldUpdateItemValues()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new PokemonRepository(context);

        var model = new Pokemon
        {
            Name = "Pikachu",
            Portrait = "Portrait",
            Height = 2,
            Weight = 5,
            Gender = new Gender() { Male = true, Female = false },
            Description = "Description",
            Generation = new Generation() { Name = "Any" },
            Specie = new Specie() { Name = "Any" },
            Categories = new List<CategoryRelation>()
            {
                new CategoryRelation()
                {
                    Category = new Category()
                    {
                        Name = "Any"
                    }
                }
            },
            Statistics = new Statistic
            {
                Hp = 100,
                Attack = 100,
                Defense = 100,
                SpecialAttack = 100,
                SpecialDefense = 100,
                Speed = 100,
                Total = 600
            }
        };

        var clone = new Pokemon
        {
            Name = "Pikachu",
            Portrait = "Portrait",
            Height = 2,
            Weight = 5,
            Gender = new Gender() { Male = true, Female = false },
            Description = "Description",
            Generation = new Generation() { Name = "Any" },
            Specie = new Specie() { Name = "Any" },
            Categories = new List<CategoryRelation>()
            {
                new CategoryRelation()
                {
                    Category = new Category()
                    {
                        Name = "Any"
                    }
                }
            },
            Statistics = new Statistic
            {
                Hp = 100,
                Attack = 100,
                Defense = 100,
                SpecialAttack = 100,
                SpecialDefense = 100,
                Speed = 100,
                Total = 600
            }
        };


        await context.Pokemons.AddAsync(model);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        model.Name = "ChangedPikachu";
        model.Portrait = "ChangedPortrait";
        model.Height = 10;
        model.Weight = 8;
        model.Gender = new Gender() { Male = false, Female = true };
        model.Description = "ChangedDescription";
        model.Generation = new Generation() { Name = "Changed" };
        model.Specie = new Specie() { Name = "Changed" };
        model.Statistics.Attack = 42;
        model.Statistics.Defense = 12;
        model.Statistics.SpecialAttack = 30;
        model.Statistics.SpecialDefense = 20;
        model.Statistics.Speed = 60;

        var result = await repository.PatchPokemonAsync(model, new CancellationToken());
        var databaseValues = await context.Pokemons
            .Include(x => x.Gender)
            .Include(x => x.Generation)
            .Include(x => x.Specie)
            .Include(x => x.Categories)
            .Include(x => x.Statistics)
            .FirstOrDefaultAsync(x => x.Name == model.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.NotEqual(clone.Name, databaseValues.Name);
        Assert.NotEqual(clone.Portrait, databaseValues.Portrait);
        Assert.NotEqual(clone.Height, databaseValues.Height);
        Assert.NotEqual(clone.Weight, databaseValues.Weight);
        Assert.NotEqual(clone.Gender.Male, databaseValues.Gender.Male);
        Assert.NotEqual(clone.Gender.Female, databaseValues.Gender.Female);
        Assert.NotEqual(clone.Specie, databaseValues.Specie);
        Assert.Equal(clone.Categories[0].Category.Id, databaseValues.Categories[0].Category.Id);
        Assert.Equal(clone.Categories[0].Category.Name, databaseValues.Categories[0].Category.Name);
        Assert.Equal(clone.Statistics.Hp, databaseValues.Statistics.Hp);
        Assert.NotEqual(clone.Statistics.Attack, databaseValues.Statistics.Attack);
        Assert.NotEqual(clone.Statistics.Defense, databaseValues.Statistics.Defense);
        Assert.NotEqual(clone.Statistics.SpecialAttack, databaseValues.Statistics.SpecialAttack);
        Assert.NotEqual(clone.Statistics.SpecialDefense, databaseValues.Statistics.SpecialDefense);
        Assert.NotEqual(clone.Statistics.Speed, databaseValues.Statistics.Speed);

        #endregion
    }

    [Fact]
    public async Task DeletePokemonAsyncShouldRemoveAnItemFromTheDatabase()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new PokemonRepository(context);

        var model = new Pokemon
        {
            Name = "Pikachu",
            Portrait = "Portrait",
            Height = 2,
            Weight = 5,
            Gender = new Gender() { Male = true, Female = false },
            Description = "Description",
            Generation = new Generation() { Name = "Any" },
            Specie = new Specie() { Name = "Any" },
            Categories = new List<CategoryRelation>()
            {
                new CategoryRelation()
                {
                    Category = new Category()
                    {
                        Name = "Any"
                    }
                }
            },
            Statistics = new Statistic
            {
                Hp = 100,
                Attack = 100,
                Defense = 100,
                SpecialAttack = 100,
                SpecialDefense = 100,
                Speed = 100,
                Total = 600
            }
        };

        await context.Pokemons.AddAsync(model);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.DeletePokemonAsync(model, new CancellationToken());
        var checkDatabase = await context.Pokemons.FirstOrDefaultAsync(x => x.Name == model.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.Null(checkDatabase);

        #endregion
    }
}