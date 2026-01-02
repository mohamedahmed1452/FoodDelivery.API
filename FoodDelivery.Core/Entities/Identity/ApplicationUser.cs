using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Address? Address { get; set; } = null;// Navigational Property [One]

    }
}
