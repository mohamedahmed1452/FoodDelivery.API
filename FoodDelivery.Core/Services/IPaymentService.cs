using FoodDelivery.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);

    }
}
