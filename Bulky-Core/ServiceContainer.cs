using AutoMapper;
using Bulky_Core.Base;
using Bulky_Core.Identity;
using Bulky_Core.Interfaces;
using Bulky_DTO.Base;
using Bulky_Infrastructure.Interfaces;
using Bulky_Models.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core
{
    public class ServiceContainer
        : IServiceContainer
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILoggerFactory loggerFactory;
        private readonly IValidatorFactory validatorFactory;
        private Dictionary<Type, object> serviceDictionary;
        private object service;
        public ServiceContainer(IUnitOfWork uow, IMapper mapper, ILoggerFactory loggerFactory,IValidatorFactory validatorFactory)
        {
            serviceDictionary = new Dictionary<Type, object>();
            this.uow = uow;
            this.mapper = mapper;
            this.loggerFactory = loggerFactory;
            this.validatorFactory = validatorFactory;
        }

        public string ErrorCode => ((BaseService)service).errorCode;

        public string ErrorMessage => ((BaseService)service).errorMessage;

        public IGenericCrudBaseService<TEntity, TDTO> GenericCrudBaseService<TEntity, TDTO>()
            where TEntity : BaseModel, new()
            where TDTO : BaseDTO, new()
        {

            if (!serviceDictionary.TryGetValue(typeof(IGenericCrudBaseService<TEntity, TDTO>), out service))
            {
                service = new GenericCrudService<TEntity, TDTO>
                    (uow, mapper, loggerFactory.CreateLogger<GenericCrudService<TEntity, TDTO>>(),validatorFactory.GetValidator<TDTO>());

                serviceDictionary.Add(typeof(IGenericCrudBaseService<TEntity, TDTO>), service);
            }

            return (IGenericCrudBaseService<TEntity, TDTO>)service;
        }

        public AccountService AccountService()
        {
            if(!serviceDictionary.TryGetValue(typeof(AccountService), out service))
            {
                service = new AccountService(uow, loggerFactory.CreateLogger<AccountService>());

                serviceDictionary.Add(typeof(AccountService), service);
            }

            return (AccountService)service;
        }
    }
}
