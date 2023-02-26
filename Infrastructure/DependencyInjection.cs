using Application.Contracts;
using Infrastructure.Persistence.DatabaseContext;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddPersistence();
    }

    private static void AddPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<PokedexDbContext>(options => 
            options.UseSqlServer(
                "Server=localhost;" +
                "Database=pokedex;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True")
        );
        
        serviceCollection.AddScoped<IGenerationRepository, GenerationRepository>();
        serviceCollection.AddScoped<ISpecieRepository, SpecieRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IPokemonRepository, PokemonRepository>();
    }
}