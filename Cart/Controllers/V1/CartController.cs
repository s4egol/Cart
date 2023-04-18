using Cart.Business.Interfaces;
using Cart.Mappers;
using Cart.Models;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(string cartId, ProductItemViewModel productItem)
        {
            ArgumentNullException.ThrowIfNull(cartId, nameof(cartId));
            ArgumentNullException.ThrowIfNull(productItem, nameof(productItem));

            _cartingService.AddItem(cartId, productItem.ToBusiness());

            return Ok();
        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string cartId, int productId)
        {
            ArgumentNullException.ThrowIfNull(cartId, nameof(cartId));

            _cartingService.DeleteItem(cartId, productId);

            return Ok();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCartItems(string cartId)
            => Ok(new CartViewModel
            {
                CartId = cartId.ToString(),
                ProductItems = _cartingService.GetItems(cartId.ToString())
                    .Select(productItem => productItem.ToView())
            });
    }
}
