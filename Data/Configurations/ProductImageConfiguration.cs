using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImage");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => new { i.ProductId, i.ImageName }).IsUnique();

        builder.Property(p => p.ImageTypeId).IsRequired();
        builder.Property(p => p.ProductId).IsRequired();
        builder.Property(p => p.ImageName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(o => o.ImageType)
            .WithMany()
            .HasForeignKey(productImage => productImage.ImageTypeId);
        builder.HasOne(o => o.Product)
            .WithMany(product => product.ProductImages)
            .HasForeignKey(productImage => productImage.ProductId);
    }
}
