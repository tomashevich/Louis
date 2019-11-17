using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Louis.Entities;
using Louis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Louis.Controllers
{
    [Log]
    [LouisExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return  await _productService.GetAll();
        }

        /// <summary>
        /// Retrieve product data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(Guid id)
        {
            return await _productService.GetById(id);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] Product product)
        {
            product.ModifiedOn = DateTime.Now;
            await _productService.Add(product);
        }
        /// <summary>
        /// Update product with new values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> Put(Guid id, [FromBody] Product updated)
        {
            var existing =  await _productService.GetById(id);
            if (existing != null)
            {
                existing.Name = updated?.Name ?? existing.Name;
                existing.Code = updated?.Code ?? existing.Code;
                existing.Price = updated?.Price ?? existing.Price;
                existing.ImageUrl = updated?.ImageUrl ?? existing.ImageUrl;
                await _productService.Update(existing);
                return new OkResult();
            }
            else
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        /// Delete product 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _productService.Delete(id);
        }
    }
}
