using System.Text.RegularExpressions;
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

    private async Task<OrderDetailDTO?> GetOrderDetailDTOForKeyAsync(Guid cartKey)
    {
        return await _context.OrderDetails
            .Where(od =>
                od.ShoppingCart != null &&
                od.ShoppingCart.CartKey == cartKey)
            .ProjectTo<OrderDetailDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
    #endregion

    #region "GET"
    [HttpGet("{cartKey}")]
    public async Task<ActionResult<ShoppingCartDTO>> GetShoppingCartAsync(Guid cartKey)
    {
        await base.DoTestsAsync();

        ShoppingCartDTO? cart = await GetShoppingCartDTOForKeyAsync(cartKey);

        return Ok(cart);
    }

    [HttpGet("{cartKey}/order")]
    public async Task<ActionResult<OrderDetailDTO>> GetOrderAsync(Guid cartKey)
    {
        await base.DoTestsAsync();

        OrderDetailDTO? orderDetail = await GetOrderDetailDTOForKeyAsync(cartKey);

        return Ok(orderDetail);
    }
    #endregion

    #region "POST"
    [HttpPost]
    public async Task<ActionResult<string>> CreateCartAsync()
    {
        await base.DoTestsAsync();

        ShoppingCart newCart = new ShoppingCart()
        {
            CartKey = Guid.NewGuid()
        };

        _context.ShoppingCarts.Add(newCart);
        await _context.SaveChangesAsync();

        //--Uses 200 status code like Ok.
        return Content(newCart.CartKey.ToString(), "text/plain");
    }

    [HttpPost("{cartKey}/lineitem")]
    public async Task<ActionResult<ShoppingCartDTO>> AddItemToCartAsync([FromBody] ShoppingCartLineItemInputDTO lineItem, Guid cartKey)
    {
        await base.DoTestsAsync();

        //--First get existing cart, or create new.
        ShoppingCart? cart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(cart => cart.CartKey == cartKey);

        if (cart == null)
        {
            return BadRequest("Cart does not exist.");
        }

        //--Add line item to cart.
        cart.LineItems.Add(new ShoppingCartLineItem()
        {
            ProductId = lineItem.ProductId,
            Quantity = lineItem.Quantity,
            SalePriceAtSale = lineItem.SalePriceAtSale,
            OriginalPriceAtSale = lineItem.OriginalPriceAtSale,
            TotalSalePrice = lineItem.TotalSalePrice,
            TotalOriginalPrice = lineItem.TotalOriginalPrice,
            SavingsPercentageAtSale = lineItem.SavingsPercentageAtSale,
            TagId = lineItem.Tag.Id
        });

        await _context.SaveChangesAsync();

        ShoppingCartDTO? cartDTO = await GetShoppingCartDTOForKeyAsync(cart.CartKey);
        return CreatedAtAction("GetShoppingCart", new { cartKey = cart.CartKey }, cartDTO);
    }

    [HttpPost("{cartKey}/order")]
    public async Task<ActionResult<OrderDetailDTO>> CreateOrderAsync([FromBody] OrderDetailInputDTO orderDetail, Guid cartKey)
    {
        await base.DoTestsAsync();

        ShoppingCart? cart = await _context.ShoppingCarts
            .Where(cart => cart.CartKey == cartKey)
            .FirstOrDefaultAsync();

        if (cart == null)
        {
            return NotFound("Cart doesn't exist.");
        }

        cart.OrderDetail = new OrderDetail()
        {
            OrderKey = Guid.NewGuid(),
            ShoppingCartId = cart.Id,

            ShippingEmail = orderDetail.ShippingEmail,
            ShippingFirstName = orderDetail.ShippingFirstName,
            ShippingLastName = orderDetail.ShippingLastName,
            ShippingAddress = orderDetail.ShippingAddress,
            ShippingSuiteApt = orderDetail.ShippingSuiteApt,
            ShippingCity = orderDetail.ShippingCity,
            ShippingState = orderDetail.ShippingState,
            ShippingZipCode = orderDetail.ShippingZipCode,
            ShippingPhone = Regex.Replace(orderDetail.ShippingPhone, @"\D", ""),

            BillingEmail = orderDetail.BillingEmail,
            BillingFirstName = orderDetail.BillingFirstName,
            BillingLastName = orderDetail.BillingLastName,
            BillingAddress = orderDetail.BillingAddress,
            BillingSuiteApt = orderDetail.BillingSuiteApt,
            BillingCity = orderDetail.BillingCity,
            BillingState = orderDetail.BillingState,
            BillingZipCode = orderDetail.BillingZipCode,
            BillingPhone = Regex.Replace(orderDetail.BillingPhone, @"\D", ""),

            CardNumber = $"**** **** **** {orderDetail.CardNumber[^4..]}",
            NameOnCard = orderDetail.NameOnCard,
            ExpirationDate = orderDetail.ExpirationDate,
            CVV = orderDetail.CVV
        };
        cart.IsComplete = true;

        await _context.SaveChangesAsync();
        
        OrderDetailDTO? orderDetailDTO = await GetOrderDetailDTOForKeyAsync(cart.CartKey);
        return CreatedAtAction("GetOrder", new { cartKey = cart.CartKey }, orderDetailDTO);
    }
    #endregion

    #region "PATCH"
    [HttpPatch("{cartKey}/lineitem")]
    public async Task<ActionResult<ShoppingCartDTO>> UpdateLineItemAsync([FromBody] ShoppingCartLineItemInputDTO lineItem, Guid cartKey)
    {
        await base.DoTestsAsync();

        ShoppingCart? cart = await _context.ShoppingCarts
            .Include(shoppingCart => shoppingCart.LineItems)
                .ThenInclude(shoppingCartLineItem => shoppingCartLineItem.Tag)
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.CartKey == cartKey);

        if (cart == null)
        {
            return NotFound("Cart doesn't exist.");
        }

        //--Get matching line item.
        ShoppingCartLineItem? cartLineItem = cart.LineItems
            .FirstOrDefault(shoppingCartLineItem =>
                shoppingCartLineItem.ProductId == lineItem.ProductId &&
                shoppingCartLineItem.TagId == lineItem.Tag.Id);

        if (cartLineItem == null)
        {
            return BadRequest("Line item doesn't exist in cart.");
        }

        cartLineItem.Quantity = lineItem.Quantity;
        cartLineItem.SalePriceAtSale = lineItem.SalePriceAtSale;
        cartLineItem.OriginalPriceAtSale = lineItem.OriginalPriceAtSale;
        cartLineItem.TotalSalePrice = lineItem.TotalSalePrice;
        cartLineItem.TotalOriginalPrice = lineItem.TotalOriginalPrice;
        cartLineItem.SavingsPercentageAtSale = lineItem.SavingsPercentageAtSale;
        await _context.SaveChangesAsync();

        ShoppingCartDTO? cartDTO = await GetShoppingCartDTOForKeyAsync(cart.CartKey);
        return Ok(cartDTO);
    }
    #endregion

    #region "DELETE"
    [HttpDelete("{cartKey}/lineitem")]
    public async Task<IActionResult> DeleteLineItemAsync([FromBody] int lineItemId, Guid cartKey)
    {
        await base.DoTestsAsync();

        //--Always sending NoContent responses because it doesn't really matter that the line item
        //--or cart doesn't exit. Let the consumer app believe it was deleted so it can continue
        //--its processing.

        ShoppingCart? cart = await _context.ShoppingCarts
            .Include(cart => cart.LineItems)
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.CartKey == cartKey);

        if (cart == null)
        {
            return NoContent();
        }

        //--Get matching line item.
        ShoppingCartLineItem? cartLineItem = cart.LineItems
            .FirstOrDefault(shoppingCartLineItem => shoppingCartLineItem.Id == lineItemId);

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