using FoodDelivery.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Identity
{
    public static class IdentityDataSeed
    {

        public static async Task SeedingAsync(UserManager<ApplicationUser> user)
        {
            if (!user.Users.Any())
            {
                var usr = new ApplicationUser()
                {
                    UserName = "MohamedAhmed",
                    Email = "ma01114600710@gmail.com",
                    DisplayName = "Mohamed",
                    PhoneNumber = "01119137448"

                };
                await user.CreateAsync(usr, "P@ssw0rd");
            }
         

        }
    }
}
