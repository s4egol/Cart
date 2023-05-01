using Cart.DataAccess.Interfaces;
using NoSql.Context;
using NoSql.Models;

namespace Cart.DataAccess.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {
        public readonly CartContext _dbContext;

        public ProductItemRepository(CartContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(ProductItem productItem)
        {
            ArgumentNullException.ThrowIfNull(productItem, nameof(productItem));

            _dbContext.ProductItems.Add(productItem);
        }

        public void Delete(string cartId, int productItemId)
            => _dbContext.ProductItems
                .DeleteExpression(x => x.ExternalId == productItemId && x.CartId == cartId);

        public IEnumerable<ProductItem> GetProductItems(string cartId)
        {
            var productItems = _dbContext.ProductItems
                .Where(product => product.CartId == cartId);

            return productItems;
        }
    }
}
