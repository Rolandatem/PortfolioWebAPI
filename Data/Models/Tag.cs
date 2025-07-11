namespace PortfolioWebAPI.Data.Models;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public int TagTypeId { get; set; }
    public TagType? TagType { get; set; }
}