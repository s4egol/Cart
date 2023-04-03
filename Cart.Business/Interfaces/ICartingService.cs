using Cart.Business.Models;

namespace Cart.Business.Interfaces
{
    public interface ICartingService
    {
        IEnumerable<ProductItemEntity> GetItems();
        void AddItem(ProductItemEntity item);
        void DeleteItem(int itemId);
    }
}
