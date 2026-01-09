using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.Core.Entities;
using FoodDelivery.Core.Entities.Order;

namespace FoodDelivery.API.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration config;

        public OrderItemUrlResolver(IConfiguration config)
        {
            this.config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return null;

            return $"{config["ApiBaseUrl"]}/{source.Product.PictureUrl}";
        }
    }
}
