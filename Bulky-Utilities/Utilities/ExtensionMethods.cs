using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Utilities.Utilities
{
    public static class ExtensionMethods
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCodeAndMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> options, (string errorCode, string errorMessage) error)
        {
            return options.WithErrorCode(error.errorCode).WithMessage(error.errorMessage);
        }
    }
}
