using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Order
{
    public class Order:BaseEntity
    {
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public ICollection<Order> Items { get; set; } = new HashSet<Order>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;


    }
}
