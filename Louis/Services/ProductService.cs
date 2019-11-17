using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Louis.Entities;
using Louis.Repositories;

namespace Louis.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<Product> _productRepository;

        public ProductService(IProductRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Add(Product product)
        {
            product.Id = Guid.NewGuid();
           await _productRepository.Add(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.Get(predicate);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }
    }
}
