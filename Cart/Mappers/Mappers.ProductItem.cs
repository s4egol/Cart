using Cart.Business.Models;
using Cart.Models;

namespace Cart.Mappers
{
    public static partial class Mappers
    {
        public static ProductItemViewModel ToView(this ProductItemEntity entity)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Image = entity.Image,
                Price = entity.Price,
                Quantity = entity.Quantity,
            };

        public static ProductItemEntity ToBusiness(this ProductItemViewModel entity)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Image = entity.Image,
                Price = entity.Price,
                Quantity = entity.Quantity,
            };
    }
}
