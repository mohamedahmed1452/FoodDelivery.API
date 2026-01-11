using FoodDelivery.Core;
using FoodDelivery.Core.Entities.Basket;
using FoodDelivery.Core.Entities.Order;
using FoodDelivery.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product= FoodDelivery.Core.Entities.Product.Product;
using System;

namespace FoodDelivery.Application.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration config,
            IBasketRepository basketRepo,
            IUnitOfWork unitOfWork)
        {
            _configuration = config;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket is null) return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod?.Cost??0;
                shippingPrice = basket.ShippingPrice;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo =_unitOfWork.GetRepository<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create Payment Intent
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await new PaymentIntentService().CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //Update Payment Intent
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };
                await new PaymentIntentService().UpdateAsync(basket.PaymentIntentId, options);
            }
            var updatedBasket = await _basketRepo.UpdateBasketAsync(basket);
            return updatedBasket;
        }
    }
}
