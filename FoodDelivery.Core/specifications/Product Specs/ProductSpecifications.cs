using FoodDelivery.Core.Entities.Product;

namespace FoodDelivery.Core.specifications.Product_Specs
{
    public class ProductSpecifications : Specifications<Product>
    {
        public ProductSpecifications(ProductSpecParams productSpecParams)
            : base(
        #region Filteration
         p =>
             (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search.ToLower())) &&
            (!productSpecParams.BrandId.HasValue || p.BrandId == productSpecParams.BrandId.Value) &&
            (!productSpecParams.CategoryId.HasValue || p.CategoryId == productSpecParams.CategoryId.Value)

         #endregion
         )
        {
            #region Sorting [asc|Desc]
            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
                AddOrderBy(p => p.Name);
            #endregion

            #region Apply Pagination

            ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageIndex - 1),
                productSpecParams.PageSize);


            #endregion


            #region Inner Join =>eager loading
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
            #endregion





        }
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
