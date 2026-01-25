using E_commerce.Sahre;
using E_commerce.Services_Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices services;

        public ProductController(IProductServices services)
        {
            this.services = services;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
        {
            var products = await services.GetALLProductsASync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById( int id)
        {
            var product= await services.GetProductByIdAsync(id);
            return Ok(product);

        }
        [HttpGet("Types")]

        public async Task<ActionResult<IEnumerable<GetType>>> GetAllTypes()
        {
            var types = await services.GetAllTypeAsync();
            return Ok(types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<GetBrand>>> GetAllBrands()
        {
            var brands = await services.GetAllBrandsAsync();
            return Ok(brands);
        }
    }
}
