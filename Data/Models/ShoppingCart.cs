namespace PortfolioWebAPI.Data.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public Guid CartKey { get; set; }
    public bool IsComplete { get; set; }

    public ICollection<ShoppingCartLineItem> LineItems { get; set; } = new HashSet<ShoppingCartLineItem>();
    public OrderDetail? OrderDetail { get; set; }
}