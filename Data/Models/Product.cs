namespace PortfolioWebAPI.Data.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = String.Empty;
    public string SKU { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
    public int Stars { get; set; }
    public int Reviews { get; set; }
    public int ColorCount { get; set; }
    public string? Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal OriginalPrice { get; set; }
    public int SavingsPercentage { get; set; }

    public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();

    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}