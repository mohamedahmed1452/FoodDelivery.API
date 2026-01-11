using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

        [Required]
        public List<BasketItemDto> Items { get; set; } = null!;
    }
}
