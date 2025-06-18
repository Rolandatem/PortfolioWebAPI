using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class SiteFilterTagTypeConfiguration : IEntityTypeConfiguration<SiteFilterTagType>
{
    public void Configure(EntityTypeBuilder<SiteFilterTagType> builder)
    {
        builder.ToTable("SiteFilterTagType");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.TagTypeId).IsUnique();

        builder.Property(p => p.TagTypeId).IsRequired();

        builder
            .HasOne(siteFilterTagType => siteFilterTagType.TagType)
            .WithMany()
            .HasForeignKey(siteFilterTagType => siteFilterTagType.TagTypeId);
    }
}
