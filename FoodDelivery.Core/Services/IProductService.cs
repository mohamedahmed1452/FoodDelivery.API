using FoodDelivery.Core.Entities.Product;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.specifications.Product_Specs;
using System;
using System.Collections.Generic;
using System.Text;
using static StackExchange.Redis.Role;

namespace FoodDelivery.Core.Services
{
    public interface IProductService
    {
        public Task<Product?> GetProductAsync(int id);
        public Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams);
        public Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
        public Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        public Task<int> GetCountAsync(ProductWithFiltersForCountSpecifications product);


    }
}
