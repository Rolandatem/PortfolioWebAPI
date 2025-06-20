namespace PortfolioWebAPI.Data.DTOs;

public record SiteFilterTagTypeDTO(
    int Id,
    string FilterType,
    int TagTypeId,
    TagTypeDTO TagType  
);