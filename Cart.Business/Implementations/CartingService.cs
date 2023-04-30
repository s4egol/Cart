using Cart.Business.Interfaces;
using Cart.Business.Mappers;
using Cart.Business.Models;
using Cart.DataAccess.Interfaces;
using Cart.DataAccess.Models;

namespace Cart.Business.Implementations
{
    public sealed class CartingService : ICartingService
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly ICartRepository _cartRepository;

        public CartingService(IProductItemRepository productItemRepository,
            ICartRepository cartRepository)
        {
            _productItemRepository = productItemRepository ?? throw new ArgumentNullException(nameof(productItemRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public void AddItem(string cartId, ProductItemEntity item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
                
            if (!_cartRepository.IsExists(cartId))
            {
                _cartRepository.Create(cartId);
            }

            CartDal? cart = _cartRepository.GetById(cartId);

            if (cart == null)
            {
                throw new Exception($"Cart with ID: {cartId} wasn't found");
            }

            var dbProductItem = item.ToDal();

            dbProductItem.CartId = cart.Id;

            _productItemRepository.Add(dbProductItem);
        }

        public void DeleteItem(string cartId, int itemId)
            => _productItemRepository.Delete(cartId, itemId);

        public IEnumerable<CartEntity>? GetAll()
            => _cartRepository.GetAll()?
                .Select(cart => cart.ToBusiness());

        public IEnumerable<ProductItemEntity> GetItems(string cartId)
        {
            var cart = _cartRepository.GetById(cartId);

            if (cart == null)
            {
                throw new Exception($"Cart with ID: {cartId} wasn't found");
            }

            var productItems = _productItemRepository.GetProductItems(cart.Id)
                .Select(productItem => productItem.ToBusiness());

            return productItems;
        }

        public void UpdateItems(IEnumerable<ProductMessage> messages)
        {
            var messageProductIds = messages.Select(message => message.Id);
            var productsById = _productItemRepository.GetProductItems(messageProductIds)
                .GroupBy(product => product.Id)
                .ToDictionary(product => product.Key, product => product.Single());

            foreach (var message in messages.Where(message => productsById.ContainsKey(message.Id)))
            {
                var product = productsById[message.Id];

                product.Name = message.Name;
                product.Price = message.Price;
                product.Quantity = message.Amount;
            }

            _productItemRepository.UpdateRange(productsById.Values);
        }
    }
}
