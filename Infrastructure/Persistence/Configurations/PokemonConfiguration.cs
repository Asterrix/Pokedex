using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        ConfigureIdFiled(builder);
        ConfigureStatisticField(builder);
    }
    
    private void ConfigureIdFiled(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(k => k.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ID");
    }
    
    private void ConfigureGenderField(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasOne(x => x.Gender);
    }

    private void ConfigureStatisticField(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasOne(x => x.Statistic);
    }
}