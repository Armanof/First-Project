using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Utilities
{
    public static class AuthExtensions
    {
        public static Guid? GetUserId(this IHttpContextAccessor context)
        {
            var userIdClaim = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid? userId = null;
            if (Guid.TryParse(userIdClaim, out var parsedGuid))
            {
                userId = parsedGuid;
            }

            return userId;
        }

        public static string? GetUserName(this IHttpContextAccessor context)
        {
            return context.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string? GetRemoteIpAddress(this IHttpContextAccessor context)
        {
            return context.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
