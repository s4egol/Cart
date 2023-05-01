﻿using Cart.Business.Interfaces;
using Cart.Mappers;
using Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Cart.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cart-management")]
    public class CartController : ControllerBase
    {
        private readonly ICartingService _cartingService;

        public CartController(ICartingService cartingService)
        {
            _cartingService = cartingService ?? throw new ArgumentNullException(nameof(cartingService));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Product in cart was added")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public IActionResult Add(string cartId, ProductItemViewModel productItem)
        {
            if (string.IsNullOrWhiteSpace(cartId) || productItem == null)
            {
                return BadRequest();
            }

            try
            {
                _cartingService.AddItem(cartId, productItem.ToBusiness());
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Product was deleted from cart")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public IActionResult Delete(string cartId, int productId)
        {
            if (string.IsNullOrWhiteSpace(cartId) || productId <= 0)
            {
                return BadRequest();
            }

            _cartingService.DeleteItem(cartId, productId);

            return Ok();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Products added in cart were loaded")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public IActionResult GetCartItems(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
            {
                return BadRequest();
            }

            var productItems = Array.Empty<ProductItemViewModel>();

            try
            {
                productItems = _cartingService.GetItems(cartId.ToString())
                    .Select(productItem => productItem.ToView())
                    .ToArray();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(new CartViewModel
            {
                CartId = cartId.ToString(),
                ProductItems = productItems
            });
        }
    }
}
