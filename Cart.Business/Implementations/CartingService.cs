using AutoMapper;
using Cart.Business.Interfaces;
using Cart.Business.Models;
using Cart.DataAccess.Interfaces;
using NoSql.Models;

namespace Cart.Business.Implementations
{
    public sealed class CartingService : ICartingService
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartingService(IProductItemRepository productItemRepository,
            ICartRepository cartRepository,
            IMapper mapper)
        {
            _productItemRepository = productItemRepository ?? throw new ArgumentNullException(nameof(productItemRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void AddItem(string cartId, ProductItemEntity item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (!_cartRepository.IsExists(cartId))
            {
                _cartRepository.Create(cartId);
            }

            NoSql.Models.Cart? cart = _cartRepository.GetById(cartId);

            if (cart == null)
            {
                throw new Exception($"Cart with ID: {cartId} wasn't found");
            }

            var dbProductItem = _mapper.Map<ProductItem>(item);

            dbProductItem.CartId = cart.Id;

            _productItemRepository.Add(dbProductItem);
        }

        public void DeleteItem(string cartId, int itemId)
        {
            _productItemRepository.Delete(cartId, itemId);
        }

        public IEnumerable<CartEntity>? GetAll()
        {
            var carts = _cartRepository.GetAll()?
                .Select(_mapper.Map<CartEntity>);

            return carts;
        }

        public IEnumerable<ProductItemEntity> GetItems(string cartId)
        {
            var cart = _cartRepository.GetById(cartId);

            if (cart == null)
            {
                throw new Exception($"Cart with ID: {cartId} wasn't found");
            }

            var productItems = _productItemRepository.GetProductItems(cart.Id)
                .Select(_mapper.Map<ProductItemEntity>);

            return productItems;
        }
    }
}
