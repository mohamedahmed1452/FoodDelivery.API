using AutoMapper;
using FoodDelivery.API.Errors;
using FoodDelivery.API.Extensions;
using FoodDelivery.API.Helpers;
using FoodDelivery.API.Middlewares;
using FoodDelivery.Core.Entities.Identity;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Infrastructure.BasketRepository;
using FoodDelivery.Infrastructure.Identity;
using FoodDelivery.Repository;
using FoodDelivery.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
#region Register all services => DI

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});// For Business Module

builder.Services.AddDbContext<ApplicationUserContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});// For Security Module


builder.Services.AddScoped<IConnectionMultiplexer>(oprtion =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { }).
    AddEntityFrameworkStores<ApplicationUserContext>();
// Swagger
builder.Services.AddSwaggerDocumentation();

// Application Services
builder.Services.AddApplicationServices();
#endregion

var app = builder.Build();

// migrate db
#region Update Database and Data Seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<StoreContext>();
    var Identity = services.GetRequiredService<ApplicationUserContext>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        await db.Database.MigrateAsync();
        await Identity.Database.MigrateAsync();
        await StoreContextSeed.SeedingAsync(db);//Seed Data Done
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        await IdentityDataSeed.SeedingAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}



#endregion


#region MiddleWare

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleware();// Swagger Middleware
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");// Handle Errors

app.UseHttpsRedirection();// Redirect HTTP to HTTPS
app.UseStaticFiles();// For wwwroot folder
app.UseAuthorization();// Authorization Middleware
app.MapControllers();// Map Controller Endpoints

#endregion
app.Run();// Run the application
