namespace PortfolioWebAPI.Data.DTOs;

public record TagDTO(
    int Id,
    string Name,
    TagTypeDTO TagType
);