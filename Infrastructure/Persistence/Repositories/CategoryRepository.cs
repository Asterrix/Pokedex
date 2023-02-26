using Application.Contracts;
using Application.Models;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly PokedexDbContext _context;

    public CategoryRepository(PokedexDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoryAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryAsync(string categoryName, CancellationToken cancellationToken)
    {
        return await _context.
            Categories.
            FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower(), cancellationToken);
    }

    public async Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return await GetCategoryAsync(category.Name, cancellationToken) ?? throw new Exception();
    }

    public async Task<bool> PatchCategoryAsync(Category updatedCategory, CancellationToken cancellationToken)
    {
        _context.Categories.Update(updatedCategory);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}