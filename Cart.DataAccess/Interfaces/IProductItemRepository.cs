using Cart.DataAccess.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface IProductItemRepository
    {
        IEnumerable<ProductItemDal> GetProductItems(string cartId);
        void Add(ProductItemDal productItem);
        void Delete(string cartId, int productItemId);
    }
}
