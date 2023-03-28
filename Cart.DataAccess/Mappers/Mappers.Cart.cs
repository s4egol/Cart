using Cart.DataAccess.Models;

namespace Cart.DataAccess.Mappers
{
    public static partial class Mappers
    {
        public static NoSql.Models.Cart ToDbState(this CartDal productItem)
            => new() { Id = productItem.Id, };

        public static CartDal ToDal(this NoSql.Models.Cart productItem)
            => new() { Id = productItem.Id, };
    }
}
