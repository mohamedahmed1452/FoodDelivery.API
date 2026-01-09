namespace FoodDelivery.Core.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order(string buyerEmail, OrderStatus status, Address shippingAddress, DeliveryMethod? deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            Status = status;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public Order()
        {
        }

        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } = null!;
        public DeliveryMethod? DeliveryMethod { get; set; } = null!;
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod?.Cost ?? 0;
        public string PaymentIntentId { get; set; } = string.Empty;


    }
}
