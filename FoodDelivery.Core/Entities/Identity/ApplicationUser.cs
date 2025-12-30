using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public Address? Address { get; set; } =null;// Navigational Property [One]

    }
}
