using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class FAQSearchResultConfiguration : IEntityTypeConfiguration<FAQSearchResult>
{
    public void Configure(EntityTypeBuilder<FAQSearchResult> builder)
    {
        builder.HasNoKey();
    }
}