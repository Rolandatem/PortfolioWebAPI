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
public class ProductController(
    IMapper _mapper,
    PortfolioDbContext _context) : PortfolioBaseController
{
    #region "GET"
    /// <summary>
    /// Gets all Products.
    /// </summary>
    /// <returns>List of Product's</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
    {
        await base.DoTestsAsync();

        return Ok(await _context.Products
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .ToListAsync());
    }

    /// <summary>
    /// Get's a product by it's ID.
    /// </summary>
    /// <param name="id">DB ID</param>
    /// <returns>Product that matches the ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
    {
        await base.DoTestsAsync();

        ProductDTO? product = await _context.Products
            .Where(p => p.Id == id)
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (product == null)
        {
            return NotFound($"No product found for id: {id}");
        }

        return Ok(product);
    }

    /// <summary>
    /// Get's a product by it's sku.
    /// </summary>
    /// <param name="sku">The product SKU.</param>
    /// <returns>Product that matches the SKU.</returns>
    [HttpGet("{sku}")]
    public async Task<ActionResult<ProductDTO>> GetProductBySkuAsync(string sku)
    {
        await base.DoTestsAsync();

        ProductDTO? product = await _context.Products
            .Where(p => p.SKU.ToLower() == sku.ToLower())
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (product == null)
        {
            return NotFound($"No product found for sku: {sku}");
        }

        return Ok(product);
    }

    /// <summary>
    /// Get's all products that belong to the category specified.
    /// </summary>
    /// <param name="categoryId">Category ID to filter products by.</param>
    /// <returns>List of products.</returns>
    [HttpGet("bycategory/{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryIdAsync(int categoryId)
    {
        await base.DoTestsAsync();

        return Ok(await _context.Products
            .Where(product => product.CategoryId == categoryId)
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .ToListAsync());
    }
    #endregion
}