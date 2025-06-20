using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.DTOs;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(
    IMapper _mapper,
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

    /// <summary>
    /// Gets a category by id.
    /// </summary>
    /// <param name="categoryId">DB ID of the category.</param>
    /// <returns>Category that matches the ID.</returns>
    [HttpGet("{categoryId:int}")]
    public async Task<ActionResult<Category>> GetCategoryByIdAsync(int categoryId)
    {
        await base.DoTestsAsync();

        CategoryDTO? category = await _context.Categories
            .Where(cat => cat.Id == categoryId)
            .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (category == null)
        {
            return NotFound($"Could not find the category with ID: {categoryId}");
        }

        return Ok(category);
    }
}