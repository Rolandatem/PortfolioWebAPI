namespace PortfolioWebAPI.Data.DTOs;

public record ShoppingCartProductDTO(
    int Id,
    string ProductName,
    string SKU
);