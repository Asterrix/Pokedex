using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureIdFiled(builder);
        ConfigureNameField(builder);
    }

    private static void ConfigureIdFiled(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(k => k.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("ID");
    }
    
    private static void ConfigureNameField(EntityTypeBuilder<Category> builder)
    {
        builder
            .Property(x => x.Name)
            .HasColumnName("Category")
            .HasMaxLength(12)
            .IsRequired();
    }
}