using FoodDelivery.API.Errors;
using FoodDelivery.Core;
using FoodDelivery.Core.Entities.Basket;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [ProducesResponseType(typeof(CustomerBasket), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [HttpPost("{basketId}")] //api/payment/basketId
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiResponse(400, "An Error With Your Basket"));
            return Ok(basket);
        }
    }
}
