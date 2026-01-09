using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.API.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; } = null!;
        [Required]
        public int DeliveryMethodId { get; set; }
    }
}
