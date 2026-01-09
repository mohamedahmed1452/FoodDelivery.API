using FoodDelivery.Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text;

namespace FoodDelivery.API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLive;

        public CachedAttribute(int timeToLive)
        {
            this.timeToLive = timeToLive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ResponseCache = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var keyCached = getKeyCachedResponse(context.HttpContext.Request);
            var result = await ResponseCache.GetCacheResponseAsync(keyCached);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }

            var executedActionContext = await next();
            if(executedActionContext.Result is OkObjectResult result1 && result1.Value is not null)
            {
                await ResponseCache.CacheResponseAsync(keyCached, result1.Value, TimeSpan.FromSeconds(timeToLive));
            }
        }

        private string getKeyCachedResponse(HttpRequest request)
        {
            var keyCached = new StringBuilder();
            keyCached.Append(request.Path);//api/product
            foreach(var (x,y) in request.Query.OrderBy(x=>x.Key))
            {
                keyCached.Append($"|{x}-{y}");
            }
            return keyCached.ToString();
        }
    }
}
