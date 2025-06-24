namespace PortfolioWebAPI.Data.DTOs;

public record ProductImageDTO(
    int Id,
    string ImageName,
    ImageTypeDTO ImageType
);