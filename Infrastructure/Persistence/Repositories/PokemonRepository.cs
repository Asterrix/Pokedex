using Application.Contracts;
using Application.Models;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly PokedexDbContext _context;

    public PokemonRepository(PokedexDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pokemon>> GetAllPokemonAsync(CancellationToken cancellationToken, string? name = "")
    {
        var result = _context.Pokemons
            .Include(x => x.Categories)
            .ThenInclude(x => x.Category);


        if (name is null)
        {
            return await result.ToListAsync(cancellationToken);
        }

        return await result.Where(n => n.Name.Contains(name)).ToListAsync(cancellationToken);
    }

    public async Task<Pokemon?> GetPokemonAsync(string pokemonName, CancellationToken cancellationToken)
    {
        return await _context
            .Pokemons
            .Include(x => x.Gender)
            .Include(x => x.Generation)
            .Include(x => x.Specie)
            .Include(x => x.Categories)
            .ThenInclude(x => x.Category)
            .Include(x => x.Statistics)
            .FirstOrDefaultAsync(p => p.Name.ToLower() == pokemonName.ToLower(), cancellationToken);
    }

    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        await _context.Pokemons.AddAsync(pokemon, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return await GetPokemonAsync(pokemon.Name, cancellationToken) ?? throw new Exception();
    }

    public async Task<bool> PatchPokemonAsync(Pokemon updatedPokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Update(updatedPokemon);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Remove(pokemon);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}