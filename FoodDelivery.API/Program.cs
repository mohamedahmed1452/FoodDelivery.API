using FoodDelivery.API.Extensions;
using FoodDelivery.API.Middlewares;
using FoodDelivery.Application.AuthService;
using FoodDelivery.Core.Entities.Identity;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.Services;
using FoodDelivery.Infrastructure.BasketRepository;
using FoodDelivery.Infrastructure.Identity;
using FoodDelivery.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
#region Register all services => DI

builder.Services.AddControllers();


#region Business Database

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});// For Business Module
builder.Services.AddSingleton<IConnectionMultiplexer>(oprtion =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));
}); //For redis Database 



#endregion

#region Security Database

builder.Services.AddDbContext<ApplicationUserContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});// For Security Module




#endregion




#region Application Register

// Application Services
builder.Services.AddApplicationServices();



#endregion


#region Basket Module


builder.Services.AddScoped<IBasketRepository, BasketRepository>();


#endregion



#region Security Register

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { }).
    AddEntityFrameworkStores<ApplicationUserContext>();
builder.Services.AddAuthSecurity(builder.Configuration);


builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

#endregion







#region Swagger Register
// Swagger
builder.Services.AddSwaggerDocumentation();
#endregion

#endregion
// في Program.cs (في الجزء بتاع الـ Services)
#region CORS Problem Registration
builder.Services.AddCors(options =>
{
options.AddPolicy("CorsPolicy", policy =>
{
policy.AllowAnyHeader()
      .AllowAnyMethod()
      .WithOrigins(
          "http://localhost:4200",      // عشان الأنغولار اللوكال بتاعك
          "http://deliveryfood.runasp.net" // عشان لما ترفع الأنغولار بعدين
      );
});
}); 
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

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerMiddleware();// Swagger Middleware
//}
app.UseSwaggerMiddleware();// Swagger Middleware

app.UseStatusCodePagesWithReExecute("/errors/{0}");// Handle Errors

app.UseHttpsRedirection();// Redirect HTTP to HTTPS
app.UseStaticFiles();// For wwwroot folder
app.UseAuthentication();// Authorization Middleware
app.UseAuthorization();
#region CORS Pipeline
app.UseCors("CorsPolicy");
#endregion

app.MapControllers();// Map Controller Endpoints
app.MapFallbackToController("Index", "Fallback");


#endregion



app.Run();// Run the application
