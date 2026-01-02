using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.Core.Entities;

namespace FoodDelivery.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration config;

        public ProductUrlResolver(IConfiguration config)
        {
            this.config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return null;

            return $"{config["ApiBaseUrl"]}/{source.PictureUrl}";
        }
    }
}



