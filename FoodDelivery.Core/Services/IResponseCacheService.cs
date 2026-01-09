using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Services
{
    public interface IResponseCacheService
    {
        public Task CacheResponseAsync(string key, object response, TimeSpan timeToLive);
        public Task<string?> GetCacheResponseAsync(string key);

    }
}
