using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GenerationConfiguration : IEntityTypeConfiguration<Generation>
{
    public void Configure(EntityTypeBuilder<Generation> builder)
    {
        ConfigureIdFiled(builder);
        ConfigureNameField(builder);
    }

    private void ConfigureIdFiled(EntityTypeBuilder<Generation> builder)
    {
        builder.HasKey(k => k.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ID");
    }
    
    private void ConfigureNameField(EntityTypeBuilder<Generation> builder)
    {
        builder
            .Property(x => x.Name)
            .HasColumnName("Generation");
    }
}