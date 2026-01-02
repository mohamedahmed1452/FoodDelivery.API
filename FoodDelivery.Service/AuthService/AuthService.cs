using FoodDelivery.Application.RefreshToken;
using FoodDelivery.Core.Entities.Identity;
using FoodDelivery.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Application.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration config;

        public AuthService(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),

            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            #region Build Security
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:AuthKey"] ?? string.Empty));
            #endregion
            var token = new JwtSecurityToken(
            #region Header
           signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature),

            #endregion
            #region PayLoad 
                audience: config["JWT:ValidAudience"],
                issuer: config["JWT:ValidIssurer"],
                expires: DateTime.UtcNow.AddMinutes(double.Parse(config["JWT:DurationInMins"] ?? "5")),
                claims: authClaims
            #endregion

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<AuthResponseDto> CreateTokenWithRefreshAsync(
           ApplicationUser user,
           UserManager<ApplicationUser> userManager)
        {
            var accessToken = await CreateTokenAsync(user, userManager);

            var refreshToken = TokenHelper.GenerateRefreshToken();
            var refreshExpires = DateTime.UtcNow.AddDays(
                double.Parse(config["JWT:RefreshTokenDurationInDays"] ?? "7")
            );

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshExpires;

            await userManager.UpdateAsync(user);

            return new AuthResponseDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshExpires
            };
        }



    }
}
