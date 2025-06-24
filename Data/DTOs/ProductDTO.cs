namespace PortfolioWebAPI.Data.DTOs;

public record ProductDTO(
    int Id,
    string ProductName,
    string SKU,
    string ImageUrl,
    int Stars,
    int Reviews,
    int ColorCount,
    string? Description,
    decimal SalePrice,
    decimal OriginalPrice,
    int SavingsPercentage,
    int CategoryId,
    ICollection<ProductTagDTO> ProductTags,
    ICollection<ProductHighlightDTO> ProductHighlights,
    ICollection<ProductImageDTO> ProductImages
);