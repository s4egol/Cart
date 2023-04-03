using Cart.Business.Interfaces;
using Cart.Business.Mappers;
using Cart.Business.Models;
using Cart.DataAccess.Interfaces;

namespace Cart.Business.Implementations
{
    public class CartingService : ICartingService
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly ICartRepository _cartRepository;

        public CartingService(IProductItemRepository productItemRepository,
            ICartRepository cartRepository)
        {
            _productItemRepository = productItemRepository ?? throw new ArgumentNullException(nameof(productItemRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public void AddItem(ProductItemEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var cart = _cartRepository.GetSingle();
            var dbProductItem = item.ToDal();

            dbProductItem.CartId = cart.Id;

            _productItemRepository.Add(dbProductItem);
        }

        public void DeleteItem(int itemId)
            => _productItemRepository.Delete(itemId);

        public IEnumerable<ProductItemEntity> GetItems()
        {
            var cart = _cartRepository.GetSingle();
            var productItems = _productItemRepository.GetProductItems(cart.Id)
                .Select(productItem => productItem.ToBusiness());

            return productItems;
        }
    }
}
