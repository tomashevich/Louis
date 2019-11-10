using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Louis.Repositories
{
    public interface IProductRepository<T> : IRepositoryBase<T> where T : class
    {       
    }
}
