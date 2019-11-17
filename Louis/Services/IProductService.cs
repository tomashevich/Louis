using Louis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Louis.Services
{
    public interface IProductService
    {
        Task <IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> predicate);
        Task<Product> GetById(Guid id);
        Task Add(Product product);
        Task  Delete(Guid id);
        Task Update(Product product);
    }
}
