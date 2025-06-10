using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrendingProductsController(
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
    public async Task<ActionResult<IEnumerable<TrendingProduct>>> GetTrendingProductsAsync()
    {
        if (base.WithDelay) { await Task.Delay(2000); }

        if (base.WithError)
        {
            throw new Exception("Test Exception from [HttpGet]GetTrendingProducts");
        }
        
        return await _context.TrendingProducts.ToListAsync();
    }
}