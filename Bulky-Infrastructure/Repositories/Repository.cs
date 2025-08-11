using Bulky_Infrastructure.Interfaces;
using Bulky_Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseModel, new()
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbset;
        private bool ignoreQueryFilters = false;
        private bool asTracking = true;

        public Repository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }

        public bool Add(T entity)
        {
            try
            {
                dbset.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var localEntity = dbset.Local.FirstOrDefault(x => x.Id == id);
                if (localEntity != null)
                {
                    context.Entry(localEntity).State = EntityState.Deleted;
                }
                else
                {
                    var entity = await Find(id);
                    dbset.Remove(entity);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<T?> Find(Guid id)
        {
            IQueryable<T> entities = dbset;

            try
            {
                if (ignoreQueryFilters)
                    entities = entities.IgnoreQueryFilters();

                if (!asTracking)
                    entities = entities.AsNoTracking();

                return await entities.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                return entities.FirstOrDefault(x => x.Id == id);
            }
        }

        public async Task<ICollection<T>?> GetAll(Expression<Func<T, bool>>? filter = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> entities = dbset;

            try
            {
                if (ignoreQueryFilters)
                    entities = entities.IgnoreQueryFilters();

                if (!asTracking)
                    entities = entities.AsNoTracking();

                if (include != null)
                    entities = include(entities);

                if(orderBy != null)
                    entities = orderBy(entities);

                if (filter != null)
                    entities = entities.Where(filter);

                return await entities.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T?> FirstOrDefault(Expression<Func<T, bool>>? filter = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> entities = dbset;

            try
            {
                if (ignoreQueryFilters)
                    entities = entities.IgnoreQueryFilters();

                if (!asTracking)
                    entities = entities.AsNoTracking();

                if (include != null)
                    entities = include(entities);

                if(orderBy != null)
                    entities = orderBy(entities);

                if (filter != null)
                    entities = entities.Where(filter);

                return await entities.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                var localEntity = dbset.Local.FirstOrDefault(x => x.Id == entity.Id);
                if (localEntity != null)
                {
                    localEntity = entity;
                    context.Entry(localEntity).State = EntityState.Modified;
                }
                else
                {
                    var entityInDb = dbset.AsNoTracking().First(x => x.Id == entity.Id);
                    entityInDb = entity;
                    dbset.Update(entityInDb);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Any(Expression<Func<T,bool>> predicate)
        {
            return await dbset.AnyAsync(predicate);
        }

        public IRepository<T> IgnoreQueryFilters()
        {
            ignoreQueryFilters = !ignoreQueryFilters;
            return this;
        }

        public IRepository<T> AsNoTracking()
        {
            asTracking = false;
            return this;
        }

        public IRepository<T> AsTracking()
        {
            asTracking = true;
            return this;
        }
    }
}
