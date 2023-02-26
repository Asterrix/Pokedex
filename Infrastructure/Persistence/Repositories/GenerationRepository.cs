using Application.Contracts;
using Application.Models;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GenerationRepository : IGenerationRepository
{
    private readonly PokedexDbContext _context;

    public GenerationRepository(PokedexDbContext context)
    {
        _context = context;
    }

    public async Task<List<Generation>> GetAllGenerationsAsync(CancellationToken cancellationToken)
    {
        return await _context.Generations.ToListAsync(cancellationToken);
    }

    public async Task<Generation?> GetGenerationAsync(string generationName, CancellationToken cancellationToken)
    {
        return await _context
            .Generations
            .FirstOrDefaultAsync(g => g.Name.ToLower() == generationName.ToLower(), cancellationToken);
    }

    public async Task<Generation> CreateGenerationAsync(Generation generation, CancellationToken cancellationToken)
    {
        await _context.Generations.AddAsync(generation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return await GetGenerationAsync(generation.Name, cancellationToken) ?? throw new Exception();
    }

    public async Task<bool> PatchGenerationAsync(Generation updatedGeneration, CancellationToken cancellationToken)
    {
        _context.Generations.Update(updatedGeneration);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteGenerationAsync(Generation generation, CancellationToken cancellationToken)
    {
        _context.Generations.Remove(generation);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}