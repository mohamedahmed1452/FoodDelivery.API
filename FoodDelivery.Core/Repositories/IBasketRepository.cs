using FoodDelivery.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string id);
        
    }
}
