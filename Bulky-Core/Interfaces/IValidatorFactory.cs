using Bulky_DTO.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Interfaces
{
    public interface IValidatorFactory
    {
        IValidator<T>? GetValidator<T>() where T : BaseDTO, new();
    }
}
