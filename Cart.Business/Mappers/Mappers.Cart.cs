using Cart.Business.Models;
using Cart.DataAccess.Models;

namespace Cart.Business.Mappers
{
    public static partial class Mappers
    {
        public static CartDal ToDal(this CartEntity cart)
            => new() { Id = cart.Id };

        public static CartEntity ToBusiness(this CartDal cart)
            => new() { Id = cart.Id };
    }
}
