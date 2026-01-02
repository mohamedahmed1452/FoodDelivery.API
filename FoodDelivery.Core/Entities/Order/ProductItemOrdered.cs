using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Order
{
    public class ProductItemOrdered
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
    }
}
