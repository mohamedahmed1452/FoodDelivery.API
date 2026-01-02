using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Order
{
    public class OrderItem:BaseEntity
    {
        public ProductItemOrdered Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
