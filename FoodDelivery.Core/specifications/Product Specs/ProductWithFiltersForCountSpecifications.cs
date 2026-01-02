using FoodDelivery.Core.Entities;

namespace FoodDelivery.Core.specifications.Product_Specs
{
    public class ProductWithFiltersForCountSpecifications : Specifications<Product>
    {
        public ProductWithFiltersForCountSpecifications(ProductSpecParams productSpecParams) : base(
       p =>
         (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search.ToLower())) &&
          (!productSpecParams.BrandId.HasValue || p.BrandId == productSpecParams.BrandId.Value) &&
          (!productSpecParams.CategoryId.HasValue || p.CategoryId == productSpecParams.CategoryId.Value)
       )
        {

        }

    }
}
