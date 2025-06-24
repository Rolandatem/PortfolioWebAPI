namespace PortfolioWebAPI.Data.DTOs;

public record ShoppingCartLineItemInputDTO(
    Guid? cartKey,
    int Quantity,
    decimal SalePriceAtSale,
    decimal OriginalPriceAtSale,
    decimal TotalSalePrice,
    decimal TotalOriginalPrice,
    int ProductId,
    TagDTO Tag
);