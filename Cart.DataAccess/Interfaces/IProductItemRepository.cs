using NoSql.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface IProductItemRepository
    {
        IEnumerable<ProductItem> GetProductItems(string cartId);
        void Add(ProductItem productItem);
        void Delete(string cartId, int productItemId);
    }
}
