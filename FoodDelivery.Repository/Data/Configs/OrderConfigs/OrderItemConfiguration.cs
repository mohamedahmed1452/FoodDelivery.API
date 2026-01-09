using FoodDelivery.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Data.Configs.OrderConfigs
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.Product, product => product.WithOwner());//Composite Attribute
            builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");

        }
    }
}
