using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class ProductHighlightConfiguration : IEntityTypeConfiguration<ProductHighlight>
{
    public void Configure(EntityTypeBuilder<ProductHighlight> builder)
    {
        builder.ToTable("ProductHighlight");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Highlight)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.ProductId).IsRequired();
    }
}
