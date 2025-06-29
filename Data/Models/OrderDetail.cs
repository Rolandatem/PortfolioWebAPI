namespace PortfolioWebAPI.Data.Models;

public class OrderDetail
{
    public int Id { get; set; }
    public Guid OrderKey { get; set; }

    public required string ShippingEmail { get; set; }
    public required string ShippingFirstName { get; set; }
    public string? ShippingLastName { get; set; }
    public required string ShippingAddress { get; set; }
    public string? ShippingSuiteApt { get; set; }
    public required string ShippingCity { get; set; }
    public required string ShippingState { get; set; }
    public required string ShippingZipCode { get; set; }
    public required string ShippingPhone { get; set; }

    public required string BillingEmail { get; set; }
    public required string BillingFirstName { get; set; }
    public string? BillingLastName { get; set; }
    public required string BillingAddress { get; set; }
    public string? BillingSuiteApt { get; set; }
    public required string BillingCity { get; set; }
    public required string BillingState { get; set; }
    public required string BillingZipCode { get; set; }
    public required string BillingPhone { get; set; }

    public required string CardNumber { get; set; }
    public required string NameOnCard { get; set; }
    public required string ExpirationDate { get; set; }
    public required string CVV { get; set; }

    public required int ShoppingCartId { get; set; }
    public ShoppingCart? ShoppingCart { get; set; }
}