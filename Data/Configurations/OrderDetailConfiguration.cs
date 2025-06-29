using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data.Configurations;

internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetail");

        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.ShoppingCartId).IsUnique();

        builder.Property(p => p.OrderKey).IsRequired();
        builder.Property(p => p.ShippingEmail)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.ShippingFirstName)
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(p => p.ShippingLastName).HasMaxLength(30);
        builder.Property(p => p.ShippingAddress)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(p => p.ShippingSuiteApt).HasMaxLength(20);
        builder.Property(p => p.ShippingCity)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.ShippingState)
            .HasMaxLength(2)
            .IsRequired();
        builder.Property(p => p.ShippingZipCode)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(p => p.ShippingPhone)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(p => p.BillingEmail)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.BillingFirstName)
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(p => p.BillingLastName).HasMaxLength(30);
        builder.Property(p => p.BillingAddress)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(p => p.BillingSuiteApt).HasMaxLength(20);
        builder.Property(p => p.BillingCity)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.BillingState)
            .HasMaxLength(2)
            .IsRequired();
        builder.Property(p => p.BillingZipCode)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(p => p.BillingPhone)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(p => p.CardNumber)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(p => p.NameOnCard)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.ExpirationDate)
            .HasMaxLength(5)
            .IsRequired();
        builder.Property(p => p.CVV)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(p => p.ShoppingCartId).IsRequired();
        builder.HasOne(orderDetail => orderDetail.ShoppingCart)
            .WithOne(shoppingCart => shoppingCart.OrderDetail)
            .HasForeignKey<OrderDetail>(orderDetail => orderDetail.ShoppingCartId);
    }
}
