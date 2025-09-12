using Bulky_Core.Interfaces;
using Bulky_DTO.Base;
using Bulky_Infrastructure.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IValidatorFactory = Bulky_Core.Interfaces.IValidatorFactory;

namespace Bulky_Core.Validators.Factory
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly Dictionary<Type, object> _cache;
        private readonly IUnitOfWork uow;

        public ValidatorFactory(IUnitOfWork uow)
        {
            _cache = new Dictionary<Type, object>();
            this.uow = uow;
        }
        public FluentValidation.IValidator<T>? GetValidator<T>() where T : BaseDTO, new()
        {
            object validator;
            var type = typeof(T);

            if (!_cache.TryGetValue(type, out validator))
            {
                var validatorType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => typeof(FluentValidation.IValidator<T>).IsAssignableFrom(t) && !t.IsAbstract);

                if (validatorType == null)
                    return null;

                validator = Activator.CreateInstance(validatorType, uow) as FluentValidation.IValidator<T>;
                _cache.Add(type, validator);
            }

            return (FluentValidation.IValidator<T>)validator;
        }
    }
}
