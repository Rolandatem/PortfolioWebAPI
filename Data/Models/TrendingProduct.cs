namespace PortfolioWebAPI.Data.Models;

public class TrendingProduct
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}