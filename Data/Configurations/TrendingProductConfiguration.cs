using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class TrendingProductConfiguration : IEntityTypeConfiguration<TrendingProduct>
{
    public void Configure(EntityTypeBuilder<TrendingProduct> builder)
    {
        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.SKU).IsUnique();

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
        builder.Property(p => p.SalePrice).IsRequired();
        builder.Property(p => p.OriginalPrice).IsRequired();
        builder.Property(p => p.SavingsPercentage).IsRequired();
        builder.Property(p => p.ShipType).IsRequired();
    }
}
