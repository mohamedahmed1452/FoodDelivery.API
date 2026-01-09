using FoodDelivery.API.Errors;
using FoodDelivery.API.Helpers;
using FoodDelivery.Application.CachingService;
using FoodDelivery.Application.OrderService;
using FoodDelivery.Application.PrdoductService;
using FoodDelivery.Core;
using FoodDelivery.Core.Services;
using FoodDelivery.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProtoBuf.Meta;
using System.Text;

namespace FoodDelivery.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));
            Services.AddScoped(typeof(IProductService), typeof(ProductService));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //builder.Services.AddAutoMapper(Mapper=> Mapper.AddProfile(new MappingProfile()));
            Services.AddAutoMapper(typeof(MappingProfile));

            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(E => E.ErrorMessage).ToList();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });//Model State Valid


            return Services;
        }

        public static IServiceCollection AddAuthSecurity(this IServiceCollection Services, IConfiguration Configuration)
        {

            Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(
    option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters()
        {

            ValidateIssuer = true,
            ValidIssuer = Configuration["JWT:ValidIssurer"],
            ValidateAudience = true,
            ValidAudience = Configuration["JWT:ValidAudience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:AuthKey"] ?? string.Empty)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero


        };
    });
            return Services;
        }

    }
}
