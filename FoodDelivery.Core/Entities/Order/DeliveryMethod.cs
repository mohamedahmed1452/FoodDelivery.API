using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Order
{
    public class DeliveryMethod:BaseEntity
    {
        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; } = null!;

    }
}
