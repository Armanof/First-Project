using Bulky_DTO.Base;
using Bulky_Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Interfaces
{
    public interface IServiceContainer
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }
        IGenericCrudBaseService<TEntity, TDTO> GenericCrudBaseService<TEntity, TDTO>()
        where TEntity : BaseModel, new()
        where TDTO : BaseDTO, new();
    }
}
