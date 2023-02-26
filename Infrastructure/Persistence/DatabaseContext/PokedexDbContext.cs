using System.Reflection;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DatabaseContext;

public class PokedexDbContext : DbContext
{
    public PokedexDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Generation> Generations { get; set; } = null!;
    public DbSet<Specie> Species { get; set; } = null!;
    public DbSet<Statistic> Statistics { get; set; } = null!;
    public DbSet<Pokemon> Pokemons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}