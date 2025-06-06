namespace PortfolioWebAPI.Data.Models;

public class TrendingProduct
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? SKU { get; set; }
    public string? ImageUrl { get; set; }
    public int Stars { get; set; }
    public int Reviews { get; set; }
    public int ColorCount { get; set; }
    public string? Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal OriginalPrice { get; set; }
    public int SavingsPercentage { get; set; }
    public int ShipType { get; set; }
}