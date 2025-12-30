using FoodDelivery.API.Errors;
using FoodDelivery.API.Helpers;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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
            });

            
            return Services;
        }
    }
}
