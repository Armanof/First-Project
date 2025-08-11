using Bulky_Models.Base;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Infrastructure.Interfaces
{
    public interface IRepository<T>
        where T : BaseModel, new()
    {
        Task<ICollection<T>?> GetAll(Expression<Func<T, bool>>? filter = null, 
                                     Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<T?> Find(Guid id);
        bool Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid id);
        IRepository<T> IgnoreQueryFilters();
        IRepository<T> AsNoTracking();
        IRepository<T> AsTracking();
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefault(Expression<Func<T, bool>>? filter = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
