using Application.Contracts;
using Application.Models;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private List<Category> _categories = new List<Category>()
    {
        new Category(){Name = "Category I"},
        new Category(){Name = "Category II"},
        new Category(){Name = "Category III"}
    };
    
    public async Task<List<Category>> GetAllCategoryAsync()
    {
        return _categories;
    }

    public async Task<Category?> GetCategoryAsync(string name)
    {
        return _categories.Find(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _categories.Add(category);
        return _categories.Find(x => x.Name == category.Name);
    }

    public async Task<bool> PatchCategoryAsync(Category category, string value)
    {
        category.Name = value;
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        _categories.Remove(category);
        return true;
    }
}