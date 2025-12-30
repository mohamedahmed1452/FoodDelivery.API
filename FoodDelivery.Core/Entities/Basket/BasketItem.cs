using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}
