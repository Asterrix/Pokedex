using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        ConfigureIdFiled(builder);
    }

    private static void ConfigureIdFiled(EntityTypeBuilder<Gender> builder)
    {
        builder.HasKey(x => x.PokemonId);
    }
}