using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Louis.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetById(Guid id);
        Task Add(T entity);
        Task Delete(Guid Id);
        Task Update(T entity);
    }
}
