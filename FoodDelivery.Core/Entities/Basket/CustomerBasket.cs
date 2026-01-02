namespace FoodDelivery.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string id)
        {
            this.Id = id;
            this.Items = new List<BasketItem>();
        }
    }
}
