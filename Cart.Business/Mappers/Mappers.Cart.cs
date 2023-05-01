using Cart.Business.Models;

namespace Cart.Business.Mappers
{
    public static partial class Mappers
    {
        public static NoSql.Models.Cart ToDal(this CartEntity cart)
            => new() { Id = cart.Id };

        public static CartEntity ToBusiness(this NoSql.Models.Cart cart)
            => new() { Id = cart.Id };
    }
}
