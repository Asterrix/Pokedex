using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SpecieConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        ConfigureIdFiled(builder);
        ConfigureNameField(builder);
    }
    
    private void ConfigureIdFiled(EntityTypeBuilder<Specie> builder)
    {
        builder.HasKey(k => k.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ID");
    }
    
    private void ConfigureNameField(EntityTypeBuilder<Specie> builder)
    {
        builder
            .Property(x => x.Name)
            .HasColumnName("Specie");
    }
}