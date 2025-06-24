namespace PortfolioWebAPI.Data.DTOs;

public record ShoppingCartLineItemDTO(
    int Id,
    int Quantity,
    decimal SalePriceAtSale,
    decimal OriginalPriceAtSale,
    decimal TotalSalePrice,
    decimal TotalOriginalPrice,
    TagDTO Tag
);