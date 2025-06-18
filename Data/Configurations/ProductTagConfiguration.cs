using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.ToTable("ProductTag");

        builder.HasKey(k => k.Id);
        builder
            .HasIndex(productTag => new { productTag.ProductId, productTag.TagId })
            .IsUnique();

        builder.Property(p => p.ProductId).IsRequired();
        builder.Property(p => p.TagId).IsRequired();

        builder
            .HasOne(productTag => productTag.Product)
            .WithMany(product => product.ProductTags)
            .HasForeignKey(productTag => productTag.ProductId);

        builder
            .HasOne(productTag => productTag.Tag)
            .WithMany()
            .HasForeignKey(productTag => productTag.TagId);
    }
}
