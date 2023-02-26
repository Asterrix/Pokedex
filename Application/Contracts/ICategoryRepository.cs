using Application.Models;

namespace Application.Contracts;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoryAsync(CancellationToken cancellationToken);
    Task<Category?> GetCategoryAsync(string categoryName, CancellationToken cancellationToken);
    Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<bool> PatchCategoryAsync(Category updatedCategory, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(Category category, CancellationToken cancellationToken);
}