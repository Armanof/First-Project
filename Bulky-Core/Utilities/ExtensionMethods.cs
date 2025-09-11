using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Utilities
{
    public static class ExtensionMethods
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCodeAndMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> options, (string errorCode, string errorMessage) error)
        {
            return options.WithErrorCode(error.errorCode).WithMessage(error.errorMessage);
        }

        public static void SetJwtCookie(this HttpResponse response, string token, int expireMinutes)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SameSite = SameSiteMode.Strict,
                Secure = false 
            };

            response.Cookies.Append("Token", token, cookieOptions);
        }
    }
}
