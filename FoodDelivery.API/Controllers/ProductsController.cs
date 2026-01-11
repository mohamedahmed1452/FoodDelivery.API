using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.API.Errors;
using FoodDelivery.API.Helpers;
using FoodDelivery.Core.Entities.Product;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.Services;
using FoodDelivery.Core.specifications.Product_Specs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(
            IProductService productService,
            IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        //[Cached(6000)]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {

            var products = await productService.GetProductsAsync(specParams);
            var data = mapper
                .Map<IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFiltersForCountSpecifications(specParams);
            var totalItems = await productService.GetCountAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(data, specParams.PageSize, specParams.PageIndex, totalItems));
        }
        //[Cached(6000)]
        [ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]// baseurl/product/id 
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            var productDto = mapper.Map<ProductToReturnDto>(product);
            return Ok(productDto);  //status code 200

        }
        [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brand = await productService.GetBrandsAsync();
            return Ok(brand);
        }

        [ProducesResponseType(typeof(IReadOnlyList<ProductCategory>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var category = await productService.GetCategoriesAsync();
            return Ok(category);
        }





    }
}
