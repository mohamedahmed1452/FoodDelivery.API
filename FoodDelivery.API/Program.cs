using FoodDelivery.Core.Repositories;
using FoodDelivery.Repository;
using FoodDelivery.Repository.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

var app = builder.Build();

// migrate db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<StoreContext>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        await db.Database.MigrateAsync();
        await StoreContextSeed.SeedingAsync(db);//Seed Data Done
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // الافتراضي بيستخدم /swagger/v1/swagger.json
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
