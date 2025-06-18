namespace PortfolioWebAPI.Data.Models;

public class TagType
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
}