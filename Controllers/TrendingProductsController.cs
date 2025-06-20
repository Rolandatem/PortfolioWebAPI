using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.DTOs;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrendingProductsController(
    IMapper _mapper,
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
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetTrendingProductsAsync()
    {
        await base.DoTestsAsync();

        return Ok(await _context.TrendingProducts
            .Select(tp => tp.Product)
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .ToListAsync());
    }
}