using Application.Contracts;
using Application.Models;

namespace Infrastructure.Persistence.Repositories;

public class SpecieRepository : ISpecieRepository
{
    private List<Specie> _species = new List<Specie>()
    {
        new Specie(){Name = "Specie I"},
        new Specie(){Name = "Specie II"},
        new Specie(){Name = "Specie III"}
    };

    
    public async Task<List<Specie>> GetAllSpecieAsync()
    {
        return _species;
    }

    public async Task<Specie?> GetSpecieAsync(string name)
    {
        return _species.Find(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Specie> CreateSpecieAsync(Specie specie)
    {
        _species.Add(specie);
        return _species.Find(x => x.Name == specie.Name);
    }

    public async Task<bool> PatchSpecieAsync(Specie specie, string value)
    {
        specie.Name = value;
        return true;
    }

    public async Task<bool> DeleteSpecieAsync(Specie specie)
    {
        _species.Remove(specie);
        return true;
    }
}