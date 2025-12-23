using FoodDelivery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.Repository.Data
{
    public static class StoreContextSeed
    {
        // Seed data methods will be implemented here in the future


        public static async Task SeedingAsync(StoreContext dbContext)
        {

            if (!dbContext.Brands.Any())//true if one element inside collection
            {
                var brandsData = File.ReadAllText("../FoodDelivery.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await dbContext.Brands.AddAsync(brand);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())//true if one element inside collection
            {
                var TypesData = File.ReadAllText("../FoodDelivery.Repository/Data/DataSeed/Categories.json");
                var tpyes = JsonSerializer.Deserialize<List<ProductCategory>>(TypesData);
                if (tpyes?.Count > 0)
                {
                    foreach (var type in tpyes)
                        await dbContext.Categories.AddAsync(type);
                    await dbContext.SaveChangesAsync();

                }
            }
            if (!dbContext.Products.Any())//true if one element inside collection
            {
                var Products = File.ReadAllText("../FoodDelivery.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(Products);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbContext.Products.AddAsync(product);
                    await dbContext.SaveChangesAsync();

                }
            }
        

        }

    }
}
