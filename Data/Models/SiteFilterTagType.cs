namespace PortfolioWebAPI.Data.Models;

public class SiteFilterTagType
{
    public int Id { get; set; }
    public required string FilterType { get; set; }

    public int TagTypeId { get; set; }
    public required TagType TagType { get; set; }
}