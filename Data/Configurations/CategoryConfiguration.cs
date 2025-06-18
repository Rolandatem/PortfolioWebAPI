using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.Property(i => i.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(i => i.ImageUrl).IsRequired();
    }
}