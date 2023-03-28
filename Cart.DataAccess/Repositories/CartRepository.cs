using Cart.DataAccess.Interfaces;
using Cart.DataAccess.Mappers;
using Cart.DataAccess.Models;
using NoSql.Context;

namespace Cart.DataAccess.Repositories
{
    public class CartRepository : ICartRepository
    {
        public readonly CartContext _dbContext;

        public CartRepository(CartContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public CartDal GetSingle() => _dbContext.Carts.FirstOrDefault()?.ToDal();
    }
}
