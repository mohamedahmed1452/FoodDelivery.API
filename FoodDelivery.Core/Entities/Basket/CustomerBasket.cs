namespace FoodDelivery.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public CustomerBasket(string id)
        {
            this.Id = id;
            this.Items = new List<BasketItem>();
        }
    }
}
