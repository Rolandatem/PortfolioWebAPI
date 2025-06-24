namespace PortfolioWebAPI.Data.Models;

public class ProductImage
{
    public int Id { get; set; }
    public int ImageTypeId { get; set; }
    public int ProductId { get; set; }
    public required string ImageName { get; set; }

    public ImageType? ImageType { get; set; }
    public Product? Product { get; set; }
}