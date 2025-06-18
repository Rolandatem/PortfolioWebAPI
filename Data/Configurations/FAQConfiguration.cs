using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

internal class FAQConfiguration : IEntityTypeConfiguration<FAQ>
{
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder.ToTable("FAQ");
        
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Question)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.UpVotes).IsRequired();
        builder.Property(p => p.DownVotes).IsRequired();
        builder.Property(p => p.IsSiteReady)
            .HasDefaultValue(false)
            .IsRequired();
    }
}