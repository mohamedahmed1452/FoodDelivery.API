using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Identity
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
