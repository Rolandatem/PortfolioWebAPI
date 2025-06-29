namespace PortfolioWebAPI.Data.Models;

public class ShoppingCartLineItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal SalePriceAtSale { get; set; }
    public decimal OriginalPriceAtSale { get; set; }
    public decimal TotalSalePrice { get; set; }
    public decimal TotalOriginalPrice { get; set; }
    public int SavingsPercentageAtSale { get; set; }

    public int ShoppingCartId { get; set; }
    public ShoppingCart? ShoppingCart { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int TagId { get; set; }
    public Tag? Tag { get; set; }
}