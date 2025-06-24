using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.DTOs;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ShoppingCartController(
    ILogger<ShoppingCartController> _logger,
    IMapper _mapper,
    PortfolioDbContext _context) : PortfolioBaseController
{
    #region "Private Methods"
    private async Task<ShoppingCartDTO?> GetShoppingCartDTOForKeyAsync(Guid cartKey)
    {
        return await _context.ShoppingCarts
            .Where(cart => cart.CartKey == cartKey)
            .ProjectTo<ShoppingCartDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
    #endregion

    #region "GET"
    [HttpGet]
    public async Task<ActionResult<ShoppingCartDTO>> GetShoppingCartAsync()
    {
        await base.DoTestsAsync();

        string? cartKey = Request.Cookies["cart_key"];
        if (cartKey.IsEmpty())
        {
            return NotFound("Customer doesn't have a shopping cart.");
        }

        ShoppingCartDTO? cart = await GetShoppingCartDTOForKeyAsync(Guid.Parse(cartKey!));

        if (cart == null)
        {
            return NotFound($"Shopping Cart with key: {cartKey} not found.");
        }

        return Ok(cart);
    }
    #endregion

    #region "POST"
    [HttpPost]
    public async Task<ActionResult<ShoppingCartDTO>> AddItemToCartAsync([FromBody] ShoppingCartLineItemInputDTO lineItem)
    {
        await base.DoTestsAsync();

        string? cartKey = Request.Cookies["cart_key"];

        //--First get existing cart, or create new.
        ShoppingCart? cart = cartKey.Exists()
            ? await _context.ShoppingCarts
                .FirstOrDefaultAsync(cart => cart.CartKey == Guid.Parse(cartKey!))
            : null;

        if (cart == null)
        {
            cart = new ShoppingCart()
            {
                CartKey = Guid.NewGuid()
            };
            _context.ShoppingCarts.Add(cart);
        }

        //--Add line item to cart.
        cart.LineItems.Add(new ShoppingCartLineItem()
        {
            Quantity = lineItem.Quantity,
            SalePriceAtSale = lineItem.SalePriceAtSale,
            OriginalPriceAtSale = lineItem.OriginalPriceAtSale,
            TotalSalePrice = lineItem.TotalSalePrice,
            TotalOriginalPrice = lineItem.TotalOriginalPrice,
            TagId = lineItem.Tag.Id
        });

        await _context.SaveChangesAsync();

        ShoppingCartDTO? cartDTO = await GetShoppingCartDTOForKeyAsync(cart.CartKey);
        return CreatedAtAction("GetShoppingCart", new { cartKey = cart.CartKey }, cartDTO);
    }
    #endregion

    #region "PATCH"
    [HttpPatch("lineitem")]
    public async Task<ActionResult<ShoppingCartDTO>> UpdateLineItemAsync([FromBody] ShoppingCartLineItemInputDTO lineItem)
    {
        await base.DoTestsAsync();

        //--Get cart
        string? cartKey = Request.Cookies["cart_key"];
        _logger.LogInformation("Cart Key from cookie: {cartKey}", cartKey);

        if (cartKey.IsEmpty())
        {
            _logger.LogError("Cart Key was empty, so sent bad request");
            return BadRequest("Customer doesn't have a cart.");
        }

        ShoppingCart? cart = await _context.ShoppingCarts
            .Include(shoppingCart => shoppingCart.LineItems)
                .ThenInclude(shoppingCartLineItem => shoppingCartLineItem.Tag)
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.CartKey == Guid.Parse(cartKey!));

        if (cart == null)
        {
            _logger.LogError("No shopping cart with key, so sneding not found.");
            return NotFound("Cart doesn't exist.");
        }

        //--Get matching line item.
        _logger.LogInformation("Current Cart Contents: {cartLineItems}", cart.LineItems);
        _logger.LogInformation("Looking for line item with the tag id: {tagId}", lineItem.Tag.Id);
        ShoppingCartLineItem? cartLineItem = cart.LineItems
            .FirstOrDefault(shoppingCartLineItem => shoppingCartLineItem.TagId == lineItem.Tag.Id);

        if (cartLineItem == null)
        {
            _logger.LogError("No matching line item, so error!");
            return BadRequest("Line item doesn't exist in cart.");
        }

        cartLineItem.Quantity = lineItem.Quantity;
        cartLineItem.SalePriceAtSale = lineItem.SalePriceAtSale;
        cartLineItem.OriginalPriceAtSale = lineItem.OriginalPriceAtSale;
        cartLineItem.TotalSalePrice = lineItem.TotalSalePrice;
        cartLineItem.TotalOriginalPrice = lineItem.TotalOriginalPrice;
        await _context.SaveChangesAsync();

        ShoppingCartDTO? cartDTO = await GetShoppingCartDTOForKeyAsync(cart.CartKey);
        return Ok(cartDTO);
    }
    #endregion

    #region "DELETE"
    [HttpDelete("lineitem")]
    public async Task<IActionResult> DeleteLineItemAsync([FromBody] ShoppingCartLineItemInputDTO lineItem)
    {
        await base.DoTestsAsync();
        
        //--Always sending NoContent responses because it doesn't really matter that the line item
        //--or cart doesn't exit. Let the consumer app believe it was deleted so it can continue
        //--its processing.

        //--Get cart
        string? cartKey = Request.Cookies["cart_key"];

        if (cartKey.IsEmpty())
        {
            return NoContent();
        }

        ShoppingCart? cart = await _context.ShoppingCarts
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.CartKey == Guid.Parse(cartKey!));

        if (cart == null)
        {
            return NoContent();
        }

        //--Get matching line item.
        ShoppingCartLineItem? cartLineItem = cart.LineItems
            .FirstOrDefault(shoppingCartLineItem => shoppingCartLineItem.TagId == lineItem.Tag.Id);

        if (cartLineItem == null)
        {
            return NoContent();
        }

        //--Remove line item from cart.
        cart.LineItems.Remove(cartLineItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    #endregion
}