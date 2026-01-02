using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.API.Errors;
using FoodDelivery.API.Helpers;
using FoodDelivery.Core.Entities;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.specifications.Product_Specs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> genericRepository;
        private readonly IGenericRepository<ProductCategory> categoryRepo;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> genericRepository,
            IGenericRepository<ProductCategory> categoryRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.categoryRepo = categoryRepo;
            this.brandRepo = brandRepo;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
      
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecifications(specParams);
            var products = await genericRepository.GetAllWithSpecAsync(spec);
            var data = mapper
                .Map<IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFiltersForCountSpecifications(specParams);
            var totalItems = await genericRepository.CountAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(data, specParams.PageSize, specParams.PageIndex, totalItems));
        }
        [ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]// baseurl/product/id 
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await genericRepository.GetWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));//status code 404
            var productDto = mapper.Map<ProductToReturnDto>(product);
            return Ok(productDto);  //status code 200

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brand = await brandRepo.GetAllAsync();
            return Ok(brand);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetCategories()
        {
            var category = await categoryRepo.GetAllAsync();
            return Ok(category);
        }





    }
}
