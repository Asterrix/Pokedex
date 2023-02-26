using Application.Models;
using Infrastructure.Persistence.Repositories;
using IntegrationTests.RepositoryTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.RepositoryTests;

public class CategoryRepositoryTests
{
    [Fact]
    public async Task CategoryRepositoryGetAllCategoryAsyncShouldReturnEmptyListCategory()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new CategoryRepository(context);

        #endregion

        #region Act

        var result = await repository.GetAllCategoryAsync(new CancellationToken());

        #endregion

        #region Assert

        Assert.IsType<List<Category>>(result);
        Assert.Empty(result);

        #endregion
    }

    [Fact]
    public async Task CategoryRepositoryGetCategoryAsyncShouldReturnNull()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new CategoryRepository(context);

        #endregion

        #region Act

        var result = await repository.GetCategoryAsync("Unknown", new CancellationToken());

        #endregion

        #region Assert

        Assert.Null(result);
        Assert.IsNotType<Category>(result);

        #endregion
    }

    [Fact]
    public async Task CategoryRepositoryGetCategoryAsyncShouldReturnOneItem()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new CategoryRepository(context);

        var sample = new Category()
        {
            Name = "Test"
        };
        await context.Categories.AddAsync(sample);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.GetCategoryAsync("Test", new CancellationToken());

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.IsType<Category>(result);
        Assert.Equal(sample, result);

        #endregion
    }

    [Fact]
    public async Task CreateCategoryAsyncShouldCreateAndStoreCategoryInsideTheDatabaseAndReturnCreatedCategory()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new CategoryRepository(context);

        var expected = new Category
        {
            Name = "Created"
        };

        #endregion

        #region Act

        var result = await repository.CreateCategoryAsync(expected, new CancellationToken());

        var checkIfStored = await context.Categories.FirstOrDefaultAsync(x => x.Name == result.Name);

        #endregion

        #region Assert

        Assert.NotNull(result);
        Assert.NotNull(checkIfStored);
        Assert.IsType<Category>(result);
        Assert.Equal(expected, result);
        Assert.Equal(result, checkIfStored);

        #endregion
    }

    [Fact]
    public async Task PatchCategoryAsyncShouldUpdateItemValues()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new CategoryRepository(context);

        Guid initialId;
        const string initialName = "Test Category";

        var category = new Category()
        {
            Name = initialName
        };

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        var y = await context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);
        initialId = y!.Id;

        #endregion

        #region Act

        category.Name = "Updated Category";

        var result = await repository.PatchCategoryAsync(category, new CancellationToken());

        var databaseValue = await context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.NotNull(databaseValue);
        Assert.Equal(initialId, databaseValue.Id);
        Assert.NotEqual(initialName, databaseValue.Name);

        #endregion
    }

    [Fact]
    public async Task DeleteCategoryAsyncShouldRemoveAnItemFromTheDatabase()
    {
        #region Arrange

        var context = ContextGenerator.Generate();
        var repository = new CategoryRepository(context);

        var model = new Category
        {
            Name = "RemoveMe"
        };

        await context.Categories.AddAsync(model);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository.DeleteCategoryAsync(model, new CancellationToken());

        var checkDatabase = await context.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);

        #endregion

        #region Assert

        Assert.True(result);
        Assert.Null(checkDatabase);

        #endregion
    }
}