using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrendingProductsController(
    ILogger<TrendingProductsController> _logger,
    PortfolioDbContext _context) : PortfolioBaseController
{

    /// <summary>
    /// Gets all current TrendingProducts.
    /// This simulates the act of retrieving trending products from the database,
    /// however here we are simulating this by just retrieving a pre-created list.
    /// </summary>
    /// <returns>List of Trending Products</returns>
    /// <exception cref="Exception">Test exception if requested.</exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetTrendingProductsAsync()
    {
        await base.DoTestsAsync();

        var data = await _context.TrendingProducts
            .Select(tp => new
            {
                tp.Product.Id,
                tp.Product.ProductName,
                tp.Product.SKU,
                tp.Product.ImageUrl,
                tp.Product.Stars,
                tp.Product.Reviews,
                tp.Product.ColorCount,
                tp.Product.Description,
                tp.Product.SalePrice,
                tp.Product.OriginalPrice,
                tp.Product.SavingsPercentage,
                ShipType = tp.Product.ProductTags
                    .Where(pt => pt.Tag.TagType.Name == "ShipType")
                    .Select(pt => pt.Tag.Id)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return Ok(data);
    }
}