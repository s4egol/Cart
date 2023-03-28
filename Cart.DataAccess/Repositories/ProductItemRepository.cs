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
            if (productItem == null)
            {
                throw new ArgumentNullException(nameof(productItem));
            }

            _dbContext.ProductItems.Add(productItem.ToDbState());
        }

        public void Delete(int productItemId)
            => _dbContext.ProductItems.Delete(productItemId);

        public IEnumerable<ProductItemDal> GetProductItems(int cartId)
            => _dbContext.ProductItems
                .AsEnumerable()
                .Select(productItem => productItem.ToDal());
    }
}
