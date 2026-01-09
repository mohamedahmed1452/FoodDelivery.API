using FoodDelivery.Core.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.Application.CachingService
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            database=redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null) return;
            var style = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonSerializer = JsonSerializer.Serialize(response, style);
             await database.StringSetAsync(key, jsonSerializer,timeToLive);
        }

        public async Task<string?> GetCacheResponseAsync(string key)
        {
            var obj = await database.StringGetAsync(key);
            if (obj.IsNullOrEmpty) return null;
            return obj;

        }
    }
}
