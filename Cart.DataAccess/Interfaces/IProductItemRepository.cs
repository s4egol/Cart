using Cart.DataAccess.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface IProductItemRepository
    {
        IEnumerable<ProductItemDal> GetProductItems(int cartId);
        void Add(ProductItemDal productItem);
        void Delete(int productItemId);
    }
}
