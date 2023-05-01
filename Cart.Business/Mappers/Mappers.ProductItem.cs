using Cart.Business.Models;
using NoSql.Models;

namespace Cart.Business.Mappers
{
    public static partial class Mappers
    {
        public static ProductItem ToDal(this ProductItemEntity productItem)
            => new()
            {
                ExternalId = productItem.Id,
                Name = productItem.Name,
                Image = productItem.Image,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
            };

        public static ProductItemEntity ToBusiness(this ProductItem productItem)
            => new()
            {
                Id = productItem.ExternalId,
                Name = productItem.Name,
                Image = productItem.Image,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
            };
    }
}
