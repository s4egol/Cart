﻿namespace Cart.DataAccess.Models
{
    public class ProductItemDal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
