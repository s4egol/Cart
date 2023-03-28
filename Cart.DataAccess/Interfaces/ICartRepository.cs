using Cart.DataAccess.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface ICartRepository
    {
        CartDal GetSingle();
    }
}
