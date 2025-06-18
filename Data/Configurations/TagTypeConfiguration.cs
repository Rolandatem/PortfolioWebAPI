using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class TagTypeConfiguration : IEntityTypeConfiguration<TagType>
{
    public void Configure(EntityTypeBuilder<TagType> builder)
    {
        builder.ToTable("TagType");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.Name).IsUnique();

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .HasMany(tagType => tagType.Tags)
            .WithOne(tag => tag.TagType)
            .HasForeignKey(tag => tag.TagTypeId);
    }
}
