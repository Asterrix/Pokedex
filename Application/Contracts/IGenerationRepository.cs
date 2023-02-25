using Application.Models;

namespace Application.Contracts;

public interface IGenerationRepository
{
    Task<List<Generation>> GetAllGenerationsAsync();
    Task<Generation?> GetGenerationAsync(string name);
    Task<Generation> CreateGenerationAsync(Generation generation);
    Task<bool> PatchGenerationAsync(Generation generation, string value);
    Task<bool> DeleteGenerationAsync(Generation generation);
}