using FoodDelivery.Core.Entities;
using FoodDelivery.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> genericRepository;

        public ProductController(IGenericRepository<Product> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await genericRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await genericRepository.GetAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }


    }
}
