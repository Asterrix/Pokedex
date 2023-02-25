using Application.Contracts;
using Infrastructure.Persistence.Repositories;
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
        serviceCollection.AddSingleton<IGenerationRepository, GenerationRepository>();
        serviceCollection.AddSingleton<ISpecieRepository, SpecieRepository>();
        serviceCollection.AddSingleton<ICategoryRepository, CategoryRepository>();
    }
}