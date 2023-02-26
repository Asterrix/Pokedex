using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.RepositoryTests.Helpers;

public static class ContextGenerator
{
    public static PokedexDbContext Generate()
    {
        var builder = new DbContextOptionsBuilder<PokedexDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
        return new PokedexDbContext(builder.Options);
    }
}