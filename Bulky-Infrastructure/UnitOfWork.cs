using Bulky_Infrastructure.Contexts;
using Bulky_Infrastructure.Interfaces;
using Bulky_Infrastructure.Repositories;
using Bulky_Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Infrastructure
{
    public class UnitOfWork
        : IUnitOfWork
    {
        private readonly BulkyContext context;
        private Dictionary<Type, object> Repositories;

        public UnitOfWork(BulkyContext context)
        {
            this.context = context;
            Repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> Repository<T>() where T : BaseModel, new()
        {
            object rep;
            if (!Repositories.TryGetValue(typeof(IRepository<T>), out rep))
            {
                rep = new Repository<T>(context);
                Repositories.Add(typeof(IRepository<T>), rep);
            }

            return (IRepository<T>)rep;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
