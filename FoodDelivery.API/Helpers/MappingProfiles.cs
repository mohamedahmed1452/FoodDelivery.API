using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.Core.Entities.Basket;
using FoodDelivery.Core.Entities.Order;
using FoodDelivery.Core.Entities.Product;
using IdentityAddress = FoodDelivery.Core.Entities.Identity.Address;
using OrderAddress = FoodDelivery.Core.Entities.Order.Address;

namespace FoodDelivery.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<IdentityAddress, AddressDto>().ReverseMap();
            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>().
                ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemUrlResolver>());
        }
    }
}
