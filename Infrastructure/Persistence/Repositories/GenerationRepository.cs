using Application.Contracts;
using Application.Models;

namespace Infrastructure.Persistence.Repositories;

public class GenerationRepository : IGenerationRepository
{
    private List<Generation> _generations = new List<Generation>()
    {
        new Generation(){Name = "Generation I"},
        new Generation(){Name = "Generation II"},
        new Generation(){Name = "Generation III"}
    };
    
    public async Task<List<Generation>> GetAllGenerationsAsync()
    {
        return _generations;
    }

    public async Task<Generation?> GetGenerationAsync(string name)
    {
        return _generations.Find(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Generation> CreateGenerationAsync(Generation generation)
    {
        _generations.Add(generation);
        return _generations.Find(x => x.Name == generation.Name);
    }

    public async Task<bool> PatchGenerationAsync(Generation generation, string value)
    {
        generation.Name = value;
        return true;
    }

    public async Task<bool> DeleteGenerationAsync(Generation generation)
    {
        _generations.Remove(generation);
        return true;
    }
}