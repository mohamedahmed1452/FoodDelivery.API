using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$",
       ErrorMessage = "Password must contain letters, numbers, and a special character")]
        public string Password { get; set; } = null!;
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;


    }
}
