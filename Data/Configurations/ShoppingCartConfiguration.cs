using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCart");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.CartKey).IsRequired();

        builder.HasMany(cart => cart.LineItems)
            .WithOne(lineItems => lineItems.ShoppingCart)
            .HasForeignKey(lineItem => lineItem.ShoppingCartId);
    }
}
