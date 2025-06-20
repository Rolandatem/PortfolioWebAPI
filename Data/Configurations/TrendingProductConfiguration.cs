using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

internal class TrendingProductConfiguration : IEntityTypeConfiguration<TrendingProduct>
{
    public void Configure(EntityTypeBuilder<TrendingProduct> builder)
    {
        builder.ToTable("TrendingProduct");
        builder.HasIndex(i => i.ProductId).IsUnique();

        builder.HasKey(k => k.Id);

        builder.Property(p => p.ProductId).IsRequired();

        builder
            .HasOne(product => product.Product)
            .WithMany() //--No reverse navigation
            .HasForeignKey(k => k.ProductId);
    }
}
