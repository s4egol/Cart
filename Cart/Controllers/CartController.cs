using Cart.Business.Interfaces;
using Cart.Mappers;
using Cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartingService _cartingService;

        public CartController(ICartingService cartingService)
        {
            _cartingService = cartingService ?? throw new ArgumentNullException(nameof(cartingService));
        }

        [HttpPost(Name = "AddNewProductItem")]
        public void Add(ProductItemViewModel productItem)
        {
            if (productItem == null)
            {
                throw new ArgumentNullException(nameof(productItem));
            }

            _cartingService.AddItem(productItem.ToBusiness());
        }

        [HttpGet(Name = "GetCartItems")]
        public IEnumerable<ProductItemViewModel> GetCartItems()
            => _cartingService.GetItems()
                .Select(productItem => productItem.ToView());
    }
}
