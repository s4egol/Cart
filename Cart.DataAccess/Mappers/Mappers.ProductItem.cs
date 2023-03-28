using Cart.DataAccess.Models;
using NoSql.Models;

namespace Cart.DataAccess.Mappers
{
    public static partial class Mappers
    {
        public static ProductItem ToDbState(this ProductItemDal productItem)
            => new()
            {
                Id = productItem.Id,
                Name = productItem.Name,
                Image = productItem.Image,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
                CartId = productItem.CartId,
            };

        public static ProductItemDal ToDal(this ProductItem productItem)
            => new()
            {
                Id = productItem.Id,
                Name = productItem.Name,
                Image = productItem.Image,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
                CartId = productItem.CartId,
            };
    }
}
