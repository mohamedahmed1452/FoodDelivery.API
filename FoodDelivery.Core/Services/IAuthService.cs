using FoodDelivery.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Core.Services
{
    public interface IAuthService
    {
        public Task<String> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
        public  Task<AuthResponseDto> CreateTokenWithRefreshAsync(
           ApplicationUser user,
           UserManager<ApplicationUser> userManager);


        
    }
}
