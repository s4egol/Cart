using Cart.Business.Models;
using Cart.DataAccess.Models;

namespace Cart.Business.Mappers
{
    public static partial class Mappers
    {
        public static ProductItemDal ToDal(this ProductItemEntity productItem)
            => new()
            {
                Id = productItem.Id,
                Name = productItem.Name,
                Image = productItem.Image,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
            };

        public static ProductItemEntity ToBusiness(this ProductItemDal productItem)
            => new()
            {
                Id = productItem.Id,
                Name = productItem.Name,
                Image = productItem.Image,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
            };
    }
}
