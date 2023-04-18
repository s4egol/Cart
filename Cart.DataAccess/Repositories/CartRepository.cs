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

        public void Create(string id)
            => _dbContext.Carts.Add(new NoSql.Models.Cart { Id = id });

        public IEnumerable<CartDal>? GetAll()
            => _dbContext.Carts
                .AsEnumerable()
                .Select(cart => cart.ToDal());

        public CartDal? GetById(string id)
            => _dbContext.Carts
                .Where(cart => cart.Id == id)
                .FirstOrDefault()?.ToDal();

        public bool IsExists(string id)
            => _dbContext.Carts
                .Where(cart => cart.Id == id)
                .Any();
    }
}
