using FoodDelivery.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Identity
{
    public class ApplicationUserContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> option) : base(option)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
