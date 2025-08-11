using Bulky_DTO.Base;
using Bulky_Models.Base;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Interfaces
{
    public interface IGenericCrudBaseService<TEntity, TDTO>
        where TEntity : BaseModel, new()
        where TDTO : BaseDTO, new()
    {
        Task<bool> Create(TDTO input);
        Task<bool> Update(TDTO input);
        Task<bool> Delete(Guid? id);
        Task<IList<TDTO>?> GetAll(Expression<Func<TEntity, bool>>? filter = null, 
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

        Task<IList<TDest>?> GetAll<TDest>(Expression<Func<TEntity, bool>>? filter = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<TDTO?> FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, 
                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
    }
}
