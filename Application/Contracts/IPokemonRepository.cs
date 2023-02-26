using Application.Models;

namespace Application.Contracts;

public interface IPokemonRepository
{
    Task<List<Pokemon>> GetAllPokemonAsync(CancellationToken cancellationToken);
    Task<Pokemon?> GetPokemonAsync(string pokemonName, CancellationToken cancellationToken);
    Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
    Task<bool> PatchPokemonAsync(Pokemon updatedPokemon, CancellationToken cancellationToken);
    Task<bool> DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken); 
}