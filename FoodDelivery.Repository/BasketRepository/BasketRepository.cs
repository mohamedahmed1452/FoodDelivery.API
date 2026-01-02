using FoodDelivery.Core.Entities.Basket;
using FoodDelivery.Core.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace FoodDelivery.Infrastructure.BasketRepository
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }


        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var createBasket = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(30));
            return !createBasket ? null : await GetBasketAsync(customerBasket.Id);

        }
    }
}
