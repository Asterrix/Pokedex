using Application.Models;

namespace Application.Contracts;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoryAsync();
    Task<Category?> GetCategoryAsync(string name);
    Task<Category> CreateCategoryAsync(Category category);
    Task<bool> PatchCategoryAsync(Category category, string value);
    Task<bool> DeleteCategoryAsync(Category category);
}