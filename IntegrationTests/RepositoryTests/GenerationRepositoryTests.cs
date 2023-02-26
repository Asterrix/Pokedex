using Application.Models;
using Infrastructure.Persistence.Repositories;
using IntegrationTests.RepositoryTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.RepositoryTests;

public class GenerationRepositoryTests
{
    [Fact]
    public async Task GenerationRepositoryGetAllGenerationAsyncShouldReturnEmptyListGeneration()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new GenerationRepository(context);

        #endregion

        #region Act

        var result = await repository.GetAllGenerationsAsync(new CancellationToken());

        #endregion

        #region Assert

        Assert.IsType<List<Generation>>(result);
        Assert.Empty(result);

        #endregion
    }

    [Fact]
    public async Task GenerationRepositoryGetGenerationAsyncShouldReturnNull()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new GenerationRepository(context);

        #endregion

        #region Act

        var result = await repository.GetGenerationAsync("Unknown", new CancellationToken());

        #endregion

        #region Assert

        Assert.Null(result);
        Assert.IsNotType<Generation>(result);

        #endregion
    }

    [Fact]
    public async Task GenerationRepositoryGetGenerationAsyncShouldReturnOneItem()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new GenerationRepository(context);

        var expected = new Generation()
        {
            Name = "Test"
        };
        await context.Generations.AddAsync(expected);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.GetGenerationAsync("Test", new CancellationToken());

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.IsType<Generation>(result);
        Assert.Equal(expected, result);

        #endregion
    }

    [Fact]
    public async Task CreateGenerationAsyncShouldCreateAndStoreGenerationInsideTheDatabaseAndReturnCreatedGeneration()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new GenerationRepository(context);

        var expected = new Generation
        {
            Name = "Created"
        };

        #endregion

        #region Act

        var result = await repository.CreateGenerationAsync(expected, new CancellationToken());
        var checkIfStored = await context.Generations.FirstOrDefaultAsync(x => x.Name == result.Name);

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.NotNull(checkIfStored);
        Assert.IsType<Generation>(result);
        Assert.Equal(expected, result);
        Assert.Equal(result, checkIfStored);

        #endregion
    }

    [Fact]
    public async Task PatchGenerationAsyncShouldUpdateItemValues()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new GenerationRepository(context);

        Guid initialId;
        const string initialName = "Test Generation";

        var generation = new Generation()
        {
            Name = initialName
        };

        await context.Generations.AddAsync(generation);
        await context.SaveChangesAsync();

        var y = await context.Generations.FirstOrDefaultAsync(x => x.Name == generation.Name);
        initialId = y!.Id;

        #endregion

        #region Act

        generation.Name = "Updated Generation";

        var result = await repository.PatchGenerationAsync(generation, new CancellationToken());

        var databaseValue = await context.Generations.FirstOrDefaultAsync(x => x.Name == generation.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.NotNull(databaseValue);
        Assert.Equal(initialId, databaseValue.Id);
        Assert.NotEqual(initialName, databaseValue.Name);

        #endregion
    }

    [Fact]
    public async Task DeleteGenerationAsyncShouldRemoveAnItemFromTheDatabase()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new GenerationRepository(context);

        var model = new Generation
        {
            Name = "RemoveMe"
        };

        await context.Generations.AddAsync(model);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.DeleteGenerationAsync(model, new CancellationToken());
        var checkDatabase = await context.Generations.FirstOrDefaultAsync(x => x.Name == model.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.Null(checkDatabase);

        #endregion
    }
}