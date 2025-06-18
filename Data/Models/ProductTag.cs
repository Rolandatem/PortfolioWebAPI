namespace PortfolioWebAPI.Data.Models;

public class ProductTag
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public required Product Product { get; set; }

    public int TagId { get; set; }
    public required Tag Tag { get; set; }
}