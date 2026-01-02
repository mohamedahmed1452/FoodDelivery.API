using FoodDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Repository.Data.Configs
{
    public class BrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();

        }
    }
}
