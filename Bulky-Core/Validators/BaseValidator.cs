using Bulky_Core.Interfaces;
using Bulky_DTO.Base;
using Bulky_Infrastructure.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Validators
{
    public abstract class BaseValidator<TDTO>
        : AbstractValidator<TDTO>
        where TDTO : BaseDTO,new()
    {
        protected readonly IUnitOfWork uow;

        public BaseValidator(IUnitOfWork uow)
        {
            this.uow = uow;
        }
    }
}
