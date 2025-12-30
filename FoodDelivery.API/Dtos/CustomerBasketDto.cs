using FoodDelivery.Core.Entities.Basket;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public List<BasketItemDto> Items { get; set; } = null!;
    }
}
