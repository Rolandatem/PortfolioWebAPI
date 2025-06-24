namespace PortfolioWebAPI.Data.DTOs;

public record ShoppingCartDTO(
    Guid CartKey,
    ICollection<ShoppingCartLineItemDTO> LineItems
);