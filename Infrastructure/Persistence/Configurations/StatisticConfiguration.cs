using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
{
    public void Configure(EntityTypeBuilder<Statistic> builder)
    {
        ConfigureIdField(builder);
        ConfigureTotalField(builder);
    }

    private void ConfigureIdField(EntityTypeBuilder<Statistic> builder)
    {
        builder.HasKey(k => k.PokemonId);

        builder
            .Property(x => x.PokemonId)
            .HasColumnName("PokemonID");
    }

    private void ConfigureTotalField(EntityTypeBuilder<Statistic> builder)
    {
        builder
            .Property(x => x.Total)
            .HasComputedColumnSql("[Hp] + [Attack] + [Defense] + [SpecialAttack] + [SpecialDefense] + [Speed]");
    }
}