namespace PortfolioWebAPI.Data.Models;

public class ProductHighlight
{
    public int Id { get; set; }
    public required string Highlight { get; set; }

    public int ProductId { get; set; }
}