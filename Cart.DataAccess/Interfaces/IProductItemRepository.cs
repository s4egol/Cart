using Cart.DataAccess.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface IProductItemRepository
    {
        IEnumerable<ProductItemDal> GetProductItems(string cartId);
        IEnumerable<ProductItemDal> GetProductItems(IEnumerable<int> productIds);
        void Add(ProductItemDal productItem);
        void UpdateRange(IEnumerable<ProductItemDal> productItems);
        void Delete(string cartId, int productItemId);
    }
}
