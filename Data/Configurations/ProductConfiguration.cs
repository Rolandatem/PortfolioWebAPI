using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.SKU)
            .IsUnique();

        builder.Property(p => p.ProductName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.SKU)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.Property(p => p.Stars).IsRequired();
        builder.Property(p => p.Reviews).IsRequired();
        builder.Property(p => p.ColorCount).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.SalePrice)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(p => p.OriginalPrice)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(p => p.SavingsPercentage).IsRequired();

        builder
            .HasMany(product => product.ProductTags)
            .WithOne(productTag => productTag.Product)
            .HasForeignKey(productTag => productTag.ProductId);

        builder
            .HasMany(product => product.ProductHighlights)
            .WithOne()
            .HasForeignKey(productHighlight => productHighlight.ProductId);

        builder
            .HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId);
    }
}
