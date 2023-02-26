using Application.Contracts;
using Application.Models;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SpecieRepository : ISpecieRepository
{
    private readonly PokedexDbContext _context;

    public SpecieRepository(PokedexDbContext context)
    {
        _context = context;
    }

    public async Task<List<Specie>> GetAllSpecieAsync(CancellationToken cancellationToken)
    {
        return await _context.Species.ToListAsync(cancellationToken);
    }

    public async Task<Specie?> GetSpecieAsync(string specieName, CancellationToken cancellationToken)
    {
        return await _context
            .Species
            .FirstOrDefaultAsync(s => s.Name.ToLower() == specieName.ToLower(), cancellationToken);
    }

    public async Task<Specie> CreateSpecieAsync(Specie specie, CancellationToken cancellationToken)
    {
        await _context.Species.AddAsync(specie, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return await GetSpecieAsync(specie.Name, cancellationToken) ?? throw new Exception();
    }

    public async Task<bool> PatchSpecieAsync(Specie updatedSpecie, CancellationToken cancellationToken)
    {
        _context.Species.Update(updatedSpecie);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteSpecieAsync(Specie specie, CancellationToken cancellationToken)
    {
        _context.Species.Remove(specie);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}