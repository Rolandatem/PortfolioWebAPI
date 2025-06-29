using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

public class ShoppingCartLineItemConfiguration : IEntityTypeConfiguration<ShoppingCartLineItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartLineItem> builder)
    {
        builder.ToTable("ShoppingCartLineItem");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => new { i.ShoppingCartId, i.TagId })
            .IsUnique();

        builder.Property(p => p.ShoppingCartId).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.ProductId).IsRequired();
        builder.Property(p => p.TagId).IsRequired();
        builder.Property(p => p.SalePriceAtSale).IsRequired();
        builder.Property(p => p.OriginalPriceAtSale).IsRequired();
        builder.Property(p => p.TotalSalePrice).IsRequired();
        builder.Property(p => p.TotalOriginalPrice).IsRequired();
        builder.Property(p => p.SavingsPercentageAtSale).IsRequired();

        builder.HasOne(lineItem => lineItem.ShoppingCart)
            .WithMany(cart => cart.LineItems)
            .HasForeignKey(lineItem => lineItem.ShoppingCartId);

        builder.HasOne(lineItem => lineItem.Product)
            .WithMany()
            .HasForeignKey(lineItem => lineItem.ProductId);

        builder.HasOne(lineItem => lineItem.Tag)
            .WithMany()
            .HasForeignKey(lineItem => lineItem.TagId);
    }
}
