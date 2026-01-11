using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.API.Errors;
using FoodDelivery.Core.Entities.Order;
using FoodDelivery.Core.Services;
using FoodDelivery.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService,
            IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        [Authorize]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var shippingAddress = mapper.Map<Address>(orderDto.ShippingAddress);
            var order = await orderService.CreatOrderAsync(BuyerEmail,
                orderDto.DeliveryMethodId,
                orderDto.BasketId,
                shippingAddress
                );
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(mapper.Map<OrderToReturnDto>(order));

        }
        
        
        [ProducesResponseType(typeof(IReadOnlyList<DeliveryMethod>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>> > GetDeliveryMethods()
        {
            var deliveryMthods = await orderService.GetDeliveryMethodsAsync();
            return deliveryMthods is not null ? Ok(deliveryMthods) : BadRequest(new ApiResponse(400));
        }

        [Authorize]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [HttpGet("{OrderId}")]
        public async Task<ActionResult<OrderToReturnDto>> GetSpecificOrderForSpecificUser(int OrderId)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderByIdForUserAsync(BuyerEmail, OrderId);
            return order is not null ? Ok(mapper.Map<OrderToReturnDto>(order)) : BadRequest(new ApiResponse(400));
        }

        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<Order>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [HttpGet("user")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForSpecificUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrdersForUserAsync(BuyerEmail);
            return order is not null ? Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(order)) : BadRequest(new ApiResponse(400));
        }

    }
}
