using Louis.Data;
using Louis.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Louis.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly  ApplicationDbContext  _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Product entity)
        {
            if (UniqueCode(entity.Code, entity.Id))
            {
                entity.Id = Guid.NewGuid();
                _context.Add(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Product with this Code already exist.");
            }

        }

        public async Task Delete(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Product.Where(predicate).ToListAsync();            
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task Update(Product entity)
        {
            if (UniqueCode(entity.Code, entity.Id) && ProductExists(entity.Id))
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Can not find product or product with this Code already exist.");
            }
        }
        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private bool UniqueCode(string code,Guid id)
        {
            return ! _context.Product.Any(e => e.Code == code && e.Id!=id);
        }
    }
}
