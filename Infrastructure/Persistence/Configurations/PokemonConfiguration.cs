using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        ConfigureIdFiled(builder);
        ConfigureGenderField(builder);
        ConfigureStatisticField(builder);
        ConfigureCategoryField(builder);
    }
    
    private static void ConfigureIdFiled(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(k => k.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ID");
    }
    
    private static void ConfigureGenderField(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasOne(x => x.Gender);
    }

    private static void ConfigureStatisticField(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasOne(x => x.Statistics);
    }

    private static void ConfigureCategoryField(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasMany(x => x.Categories);
    }
}