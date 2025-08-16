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

        public static void SetJwtCookie(this HttpResponse response, string token,int expiresMinute = 60)
        {
            response.Cookies.Append("jwt", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(expiresMinute)
            });
        }
    }
}
