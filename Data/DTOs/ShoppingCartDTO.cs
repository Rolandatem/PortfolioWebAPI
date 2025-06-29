namespace PortfolioWebAPI.Data.DTOs;

public record ShoppingCartDTO(
    Guid CartKey,
    bool IsComplete,
    ICollection<ShoppingCartLineItemDTO> LineItems
);