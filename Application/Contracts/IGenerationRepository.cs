using Application.Models;

namespace Application.Contracts;

public interface IGenerationRepository
{
    Task<List<Generation>> GetAllGenerationsAsync(CancellationToken cancellationToken);
    Task<Generation?> GetGenerationAsync(string generationName, CancellationToken cancellationToken);
    Task<Generation> CreateGenerationAsync(Generation generation, CancellationToken cancellationToken);
    Task<bool> PatchGenerationAsync(Generation updatedGeneration, CancellationToken cancellationToken);
    Task<bool> DeleteGenerationAsync(Generation generation, CancellationToken cancellationToken);
}