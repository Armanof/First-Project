using Bulky_Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : BaseModel, new();
        Task SaveChangesAsync();
    }
}
