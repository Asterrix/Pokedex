using Application.Models;

namespace Application.Contracts;

public interface ISpecieRepository
{
    Task<List<Specie>> GetAllSpecieAsync(CancellationToken cancellationToken);
    Task<Specie?> GetSpecieAsync(string specieName, CancellationToken cancellationToken);
    Task<Specie> CreateSpecieAsync(Specie specie, CancellationToken cancellationToken);
    Task<bool> PatchSpecieAsync(Specie updatedSpecie, CancellationToken cancellationToken);
    Task<bool> DeleteSpecieAsync(Specie specie, CancellationToken cancellationToken);
}