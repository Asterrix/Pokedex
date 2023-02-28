using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CategoryRelationConfiguration : IEntityTypeConfiguration<CategoryRelation>
{
    public void Configure(EntityTypeBuilder<CategoryRelation> builder)
    {
        ConfigureIdField(builder);
        ConfigureCategoryField(builder);
    }

    private static void ConfigureIdField(EntityTypeBuilder<CategoryRelation> builder)
    {
        builder.HasKey(k => k.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("RelationID");
        
        builder
            .Property(x => x.PokemonId)
            .HasColumnName("PokemonID");
    }

    private static void ConfigureCategoryField(EntityTypeBuilder<CategoryRelation> builder)
    {
        builder.HasOne(x => x.Category);
    }
}