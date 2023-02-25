using Application.Models;

namespace Application.Contracts;

public interface ISpecieRepository
{
    Task<List<Specie>> GetAllSpecieAsync();
    Task<Specie?> GetSpecieAsync(string name);
    Task<Specie> CreateSpecieAsync(Specie specie);
    Task<bool> PatchSpecieAsync(Specie specie, string value);
    Task<bool> DeleteSpecieAsync(Specie specie);
}