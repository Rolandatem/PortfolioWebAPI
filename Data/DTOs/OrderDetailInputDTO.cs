namespace PortfolioWebAPI.Data.DTOs;

public record OrderDetailInputDTO(
    string ShippingEmail,
    string ShippingFirstName,
    string? ShippingLastName,
    string ShippingAddress,
    string? ShippingSuiteApt,
    string ShippingCity,
    string ShippingState,
    string ShippingZipCode,
    string ShippingPhone,

    string BillingEmail,
    string BillingFirstName,
    string? BillingLastName,
    string BillingAddress,
    string? BillingSuiteApt,
    string BillingCity,
    string BillingState,
    string BillingZipCode,
    string BillingPhone,

    string CardNumber,
    string NameOnCard,
    string ExpirationDate,
    string CVV
);