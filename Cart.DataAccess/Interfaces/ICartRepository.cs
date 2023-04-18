using Cart.DataAccess.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface ICartRepository
    {
        IEnumerable<CartDal>? GetAll();
        CartDal? GetById(string id);
        bool IsExists(string id);
        void Create(string id);
    }
}
