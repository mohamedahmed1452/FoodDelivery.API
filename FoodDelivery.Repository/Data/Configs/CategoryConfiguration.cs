using FoodDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Repository.Data.Configs
{
    public class CategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();

        }
    }
}
