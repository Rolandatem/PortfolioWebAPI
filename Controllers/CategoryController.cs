using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(
    PortfolioDbContext _context) : PortfolioBaseController
{
    /// <summary>
    /// Gets all product categories.
    /// </summary>
    /// <returns>List of Product Categories</returns>
    /// <exception cref="Exception">Test exception if requested.</exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
    {
        await base.DoTestsAsync();

        return await _context.Categories.ToListAsync();
    }
}