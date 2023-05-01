using Cart.Business.Interfaces;
using Cart.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cart-management")]
    public class CartController : ControllerBase
    {
        private readonly ICartingService _cartingService;

        public CartController(ICartingService cartingService)
        {
            _cartingService = cartingService ?? throw new ArgumentNullException(nameof(cartingService));
        }

        [HttpGet(Name = "AAAA")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCartItems(string cartId)
        {
            var productItems = _cartingService.GetItems(cartId.ToString())
                .Select(productItem => productItem.ToView());

            return Ok(productItems);
        }
    }
}
