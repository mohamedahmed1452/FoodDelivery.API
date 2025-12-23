using FoodDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Repository.Data.Configs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.PictureUrl).HasMaxLength(200);
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId);
        }
    }
}
