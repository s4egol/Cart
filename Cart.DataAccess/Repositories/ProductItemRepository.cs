using Cart.DataAccess.Interfaces;
using Cart.DataAccess.Mappers;
using Cart.DataAccess.Models;
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

        public void Add(ProductItemDal productItem)
        {
            ArgumentNullException.ThrowIfNull(productItem, nameof(productItem));

            _dbContext.ProductItems.Add(productItem.ToDbState());
        }

        public void UpdateRange(IEnumerable<ProductItemDal> productItems)
        {
            var productItemIds = productItems.Select(productItem => productItem.Id);
            var products = _dbContext.ProductItems
                .Where(product => productItemIds.Contains(product.ExternalId))
                .GroupBy(product => product.ExternalId)
                .ToDictionary(products => products.Key, products => products.ToArray());

            if (!products.Any())
            {
                return;
            }

            foreach (var productItem in productItems)
            {
                var dbProducts = products.GetValueOrDefault(productItem.Id) ?? Array.Empty<ProductItem>();

                foreach(var dbProduct in dbProducts)
                {
                    dbProduct.Name = productItem.Name;
                    dbProduct.Price = productItem.Price;
                    dbProduct.Quantity = productItem.Quantity;
                }
            }

            _dbContext.ProductItems.Update(products.SelectMany(product => product.Value));
        }

        public void Delete(string cartId, int productItemId)
            => _dbContext.ProductItems
                .DeleteExpression(x => x.ExternalId == productItemId && x.CartId == cartId);

        public IEnumerable<ProductItemDal> GetProductItems(string cartId)
            => _dbContext.ProductItems
                .Where(product => product.CartId == cartId)
                .Select(productItem => productItem.ToDal());

        public IEnumerable<ProductItemDal> GetProductItems(IEnumerable<int> productIds)
            => _dbContext.ProductItems
                .Where(product => productIds.Contains(product.ExternalId))
                .Select(productItem => productItem.ToDal());
    }
}
