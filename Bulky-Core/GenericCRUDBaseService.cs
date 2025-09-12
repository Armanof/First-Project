using AutoMapper;
using Bulky_Core.Base;
using Bulky_Core.Interfaces;
using Bulky_Core.Messages;
using Bulky_Core.Validators;
using Bulky_DTO.Base;
using Bulky_Infrastructure.Interfaces;
using Bulky_Models.Base;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core
{
    public class GenericCrudService<TEntity, TDTO>
        : BaseService, IGenericCrudBaseService<TEntity, TDTO>
        where TEntity : BaseModel, new()
        where TDTO : BaseDTO, new()
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IValidator<TDTO> validator;


        public GenericCrudService(IUnitOfWork uow, IMapper mapper, ILogger logger,IValidator<TDTO> validator)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.logger = logger;
            this.validator = validator;
        }

        public virtual async Task<bool> Create(TDTO input)
        {
            try
            {

                if (!await Validation(input))
                {
                    return false;
                }

                var entity = mapper.Map<TEntity>(input);

                entity.StringNormalize();

                var result = uow.Repository<TEntity>().Add(entity);

                await uow.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure(false, GeneralMessages.DefaultError());
            }
        }

        public virtual async Task<bool> Delete(Guid? id)
        {
            try
            {
                if (id == null)
                    return Failure(false, GeneralMessages.DefaultError());

                await uow.Repository<TEntity>().Delete(id.Value);
                await uow.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure(false, GeneralMessages.DefaultError());
            }
        }

        public virtual async Task<TDTO?> FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, 
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            try
            {
                var result = await uow.Repository<TEntity>().FirstOrDefault(filter, include,orderBy);
                return mapper.Map<TDTO>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure((TDTO?)null, GeneralMessages.DefaultError());
            }
        }

        public virtual async Task<IList<TDTO>?> GetAll(Expression<Func<TEntity, bool>>? filter = null, 
                                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            try
            {
                var result = await uow.Repository<TEntity>().GetAll(filter, include, orderBy);
                return mapper.Map<IList<TDTO>>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure((IList<TDTO>?)null, GeneralMessages.DefaultError());
            }
        }

        public virtual async Task<IList<TDest>?> GetAll<TDest>(Expression<Func<TEntity, bool>>? filter = null,
                                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            try
            {
                var result = await uow.Repository<TEntity>().GetAll(filter, include, orderBy);
                return mapper.Map<IList<TDest>>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure((IList<TDest>?)null, GeneralMessages.DefaultError());
            }
        }

        public virtual async Task<bool> Update(TDTO input)
        {
            try
            {
                if (!await Validation(input))
                {
                    return false;
                }

                var entity = mapper.Map<TEntity>(input);

                entity.StringNormalize();

                await uow.Repository<TEntity>().Update(entity);

                await uow.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure(false, GeneralMessages.DefaultError());
            }

        }

        public virtual async Task<bool> Any(Expression<Func<TEntity,bool>> predicate)
        {
            try
            {
                return await uow.Repository<TEntity>().Any(predicate);
            }
            catch (Exception ex)
            {
                logger.LogError(JsonConvert.SerializeObject(ex));
                return Failure(false, GeneralMessages.DefaultError());
            }
        }

        private async Task<bool> Validation(TDTO input)
        {
            if(validator is null)
                return true;

            var result = await validator.ValidateAsync(input);
            if (!result.IsValid)
            {
                return Failure(false ,"",string.Join("<br>", result.Errors.Select(x => x.ErrorMessage)));
            }

            return result.IsValid;
        }
    }
}
