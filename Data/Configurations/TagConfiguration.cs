using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tag");

        builder.HasKey(k => k.Id);
        builder.HasIndex(tag => new { tag.TagTypeId, tag.Name }).IsUnique();

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.TagTypeId).IsRequired();

        builder
            .HasOne(tag => tag.TagType)
            .WithMany(tagType => tagType.Tags)
            .HasForeignKey(tag => tag.TagTypeId);
    }
}
