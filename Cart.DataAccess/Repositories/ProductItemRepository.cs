using Cart.DataAccess.Interfaces;
using Cart.DataAccess.Mappers;
using Cart.DataAccess.Models;
using NoSql.Context;

namespace Cart.DataAccess.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {
        public readonly CartContext _dbContext;

        public ProductItemRepository(CartContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(ProductItemDal productItem)
        {
            ArgumentNullException.ThrowIfNull(productItem, nameof(productItem));

            _dbContext.ProductItems.Add(productItem.ToDbState());
        }

        public void Delete(string cartId, int productItemId)
            => _dbContext.ProductItems
                .DeleteExpression(x => x.ExternalId == productItemId && x.CartId == cartId);

        public IEnumerable<ProductItemDal> GetProductItems(string cartId)
            => _dbContext.ProductItems
                .Where(product => product.CartId == cartId)
                .Select(productItem => productItem.ToDal());
    }
}
