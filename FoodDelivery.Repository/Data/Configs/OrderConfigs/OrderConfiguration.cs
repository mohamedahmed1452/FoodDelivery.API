using FoodDelivery.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Data.Configs.OrderConfigs
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status).HasConversion(
                (Ostatus) => Ostatus.ToString(),
                (Ostatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));
            builder.OwnsOne(o => o.ShippingAddress, sa => sa.WithOwner());//Composite Attribute
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
