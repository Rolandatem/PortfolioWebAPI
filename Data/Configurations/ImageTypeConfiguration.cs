using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class ImageTypeConfiguration : IEntityTypeConfiguration<ImageType>
{
    public void Configure(EntityTypeBuilder<ImageType> builder)
    {
        builder.ToTable("ImageType");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.Name).IsUnique();

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
