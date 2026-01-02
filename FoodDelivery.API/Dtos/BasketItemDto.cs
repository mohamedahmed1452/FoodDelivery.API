using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public string PictureUrl { get; set; } = null!;
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be One Item At Least")]

        public int Quentaty { get; set; }
        [Required]
        public string Brand { get; set; } = null!;
        [Required]
        public string Category { get; set; } = null!;


    }
}
