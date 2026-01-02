using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.Application.RefreshToken
{
    public class TokenHelper
    {
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
