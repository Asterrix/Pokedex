using Application.Models;
using Infrastructure.Persistence.Repositories;
using IntegrationTests.RepositoryTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.RepositoryTests;

public class SpecieRepositoryTests
{
    [Fact]
    public async Task SpecieRepositoryGetAllSpecieAsyncShouldReturnEmptyListSpecie()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new SpecieRepository(context);

        #endregion

        #region Act

        var result = await repository.GetAllSpecieAsync(new CancellationToken());

        #endregion

        #region Assert

        Assert.IsType<List<Specie>>(result);
        Assert.Empty(result);

        #endregion
    }

    [Fact]
    public async Task SpecieRepositoryGetSpecieAsyncShouldReturnNull()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new SpecieRepository(context);

        #endregion

        #region Act

        var result = await repository.GetSpecieAsync("Unknown", new CancellationToken());

        #endregion

        #region Assert

        Assert.Null(result);
        Assert.IsNotType<Specie>(result);

        #endregion
    }

    [Fact]
    public async Task SpecieRepositoryGetSpecieAsyncShouldReturnOneItem()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new SpecieRepository(context);

        var expected = new Specie()
        {
            Name = "Test"
        };
        await context.Species.AddAsync(expected);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.GetSpecieAsync("Test", new CancellationToken());

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.IsType<Specie>(result);
        Assert.Equal(expected, result);

        #endregion
    }

    [Fact]
    public async Task CreateSpecieAsyncShouldCreateAndStoreSpecieInsideTheDatabaseAndReturnCreatedSpecie()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new SpecieRepository(context);

        var expected = new Specie
        {
            Name = "Created"
        };

        #endregion

        #region Act

        var result = await repository.CreateSpecieAsync(expected, new CancellationToken());
        var checkIfStored = await context.Species.FirstOrDefaultAsync(x => x.Name == result.Name);

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.NotNull(checkIfStored);
        Assert.IsType<Specie>(result);
        Assert.Equal(expected, result);
        Assert.Equal(result, checkIfStored);

        #endregion
    }
    
    [Fact]
    public async Task PatchSpecieAsyncShouldUpdateItemValues()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new SpecieRepository(context);

        Guid initialId;
        const string initialName = "Test Specie";

        var specie = new Specie()
        {
            Name = initialName
        };

        await context.Species.AddAsync(specie);
        await context.SaveChangesAsync();

        var y = await context.Species.FirstOrDefaultAsync(x => x.Name == specie.Name);
        initialId = y!.Id;

        #endregion

        #region Act

        specie.Name = "Updated Specie";

        var result = await repository.PatchSpecieAsync(specie, new CancellationToken());

        var databaseValue = await context.Species.FirstOrDefaultAsync(x => x.Name == specie.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.NotNull(databaseValue);
        Assert.Equal(initialId, databaseValue.Id);
        Assert.NotEqual(initialName, databaseValue.Name);

        #endregion
    }

    [Fact]
    public async Task DeleteSpecieAsyncShouldRemoveAnItemFromTheDatabase()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new SpecieRepository(context);

        var model = new Specie
        {
            Name = "RemoveMe"
        };

        await context.Species.AddAsync(model);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.DeleteSpecieAsync(model, new CancellationToken());

        var checkDatabase = await context.Species.FirstOrDefaultAsync(x => x.Name == model.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.Null(checkDatabase);

        #endregion
    }
}