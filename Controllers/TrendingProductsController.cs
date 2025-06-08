
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
    /// <param name="withDelay">Adds a 2 second delay for testing latency.</param>
    /// <param name="withError">Causes an exception to test errors.</param>
    /// <returns>List of Trending Products</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrendingProduct>>> GetTrendingProductsAsync(
        [FromQuery] bool withDelay = false,
        [FromQuery] bool withError = false)
    {
        if (withError)
        {
            throw new Exception("Test Exception");
        }

        if (withDelay) { await Task.Delay(2000); }
        
        return await _context.TrendingProducts.ToListAsync();
    }
}