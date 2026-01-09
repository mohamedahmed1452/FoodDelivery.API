using FoodDelivery.Core;
using FoodDelivery.Core.Entities.Product;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.Services;
using FoodDelivery.Core.specifications.Product_Specs;
using FoodDelivery.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.PrdoductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand>().GetAllAsync();
            return brands;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        {
            var categorys = await unitOfWork.GetRepository<ProductCategory>().GetAllAsync();
            return categorys;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await unitOfWork.GetRepository<Product>().GetWithSpecAsync(spec);
            return product is not null ? product : null;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductSpecifications(specParams);
            var products = await unitOfWork.GetRepository<Product>().GetAllWithSpecAsync(spec);
            return products;
        }

        public async Task<int> GetCountAsync(ProductWithFiltersForCountSpecifications product)
        {
            return await unitOfWork.GetRepository<Product>().CountAsync(product);
        }
    }
}
