using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.API.Errors;
using FoodDelivery.Core.Entities.Basket;
using FoodDelivery.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,
            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket == null ? new CustomerBasket(id) : Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var customerBasket = mapper.Map<CustomerBasket>(customerBasketDto);
            var createdBasket = await basketRepository.UpdateBasketAsync(customerBasket);
            return createdBasket == null ? BadRequest(new ApiResponse(400)) : Ok(createdBasket);
        }
        [HttpDelete]
        public async Task<bool> DeleteBasket(string Id)
        {
            return await basketRepository.DeleteBasketAsync(Id);
        }
    }
}
