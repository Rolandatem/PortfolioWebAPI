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

    private async Task<OrderDetailDTO?> GetOrderDetailDTOForKeyAsync(Guid orderKey)
    {
        return await _context.OrderDetails
            .Where(od => od.OrderKey == orderKey)
            .ProjectTo<OrderDetailDTO>(_mapper.ConfigurationProvider)
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
            return Ok(null);
        }

        ShoppingCartDTO? cart = await GetShoppingCartDTOForKeyAsync(Guid.Parse(cartKey!));

        if (cart == null)
        {
            return Ok(null);
        }

        return Ok(cart);
    }

    [HttpGet("{cartKey}")]
    public async Task<ActionResult<ShoppingCartDTO>> GetShoppingCartByKeyAsync(Guid cartKey)
    {
        await base.DoTestsAsync();

        ShoppingCartDTO? cart = await GetShoppingCartDTOForKeyAsync(cartKey);

        return Ok(cart);
    }

    [HttpGet("order")]
    public async Task<ActionResult<OrderDetailDTO>> GetOrderAsync()
    {
        await base.DoTestsAsync();

        string? orderKey = Request.Cookies["order_key"];
        if (orderKey == null)
        {
            return Ok(null);
        }

        OrderDetailDTO? orderDetail = await GetOrderDetailDTOForKeyAsync(Guid.Parse(orderKey));
        if (orderDetail == null)
        {
            return Ok(null);
        }

        return Ok(orderDetail);
    }

    [HttpGet("order/{orderKey}")]
    public async Task<ActionResult<OrderDetailDTO>> GetOrderByKeyAsync(Guid orderKey)
    {
        await base.DoTestsAsync();

        OrderDetailDTO? orderDetail = await GetOrderDetailDTOForKeyAsync(orderKey);

        return Ok(orderDetail);
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

    [HttpPost("order")]
    public async Task<ActionResult<OrderDetailDTO>> CreateOrderAsync([FromBody] OrderDetailInputDTO orderDetail)
    {
        await base.DoTestsAsync();

        //--Get cart
        string? cartKey = Request.Cookies["cart_key"];
        if (cartKey.IsEmpty())
        {
            return BadRequest("Customer doesn't have a cart.");
        }

        ShoppingCart? cart = await _context.ShoppingCarts
            .Where(cart => cart.CartKey == Guid.Parse(cartKey!))
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
        
        OrderDetailDTO? orderDetailDTO = await GetOrderDetailDTOForKeyAsync(cart.OrderDetail.OrderKey);
        return CreatedAtAction("GetOrder", new { orderKey = cart.OrderDetail.OrderKey }, orderDetailDTO);
    }
    #endregion

    #region "PATCH"
    [HttpPatch("lineitem")]
    public async Task<ActionResult<ShoppingCartDTO>> UpdateLineItemAsync([FromBody] ShoppingCartLineItemInputDTO lineItem)
    {
        await base.DoTestsAsync();

        //--Get cart
        string? cartKey = Request.Cookies["cart_key"];

        if (cartKey.IsEmpty())
        {
            return BadRequest("Customer doesn't have a cart.");
        }

        ShoppingCart? cart = await _context.ShoppingCarts
            .Include(shoppingCart => shoppingCart.LineItems)
                .ThenInclude(shoppingCartLineItem => shoppingCartLineItem.Tag)
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.CartKey == Guid.Parse(cartKey!));

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
    [HttpDelete("lineitem")]
    public async Task<IActionResult> DeleteLineItemAsync([FromBody] int lineItemId)
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
            .Include(cart => cart.LineItems)
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.CartKey == Guid.Parse(cartKey!));

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