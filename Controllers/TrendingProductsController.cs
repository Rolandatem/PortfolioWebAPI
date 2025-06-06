
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrendingProductsController(
    PortfolioDbContext _context) : ControllerBase
{

    /// <summary>
    /// Gets all current TrendingProducts.
    /// This simulates the act of retrieving trending products from the database,
    /// however here we are simulating this by just retrieving a pre-created list.
    /// </summary>
    /// <returns>List of Trending Products</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrendingProduct>>> GetTrendingProductsAsync()
    {
        var data = await _context.TrendingProducts.ToListAsync();
        return data;
        // return await _context.TrendingProducts
        //     .ToListAsync();
    }
}