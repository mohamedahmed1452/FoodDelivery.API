using FoodDelivery.API.Errors;
using System.Text.Json;

namespace FoodDelivery.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //take action on request


            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = env.IsDevelopment()
                    ? new ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiExceptionResponse(context.Response.StatusCode);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);

            }
            //take action on response
        }
    }
}
