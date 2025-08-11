using Bulky_Core;
using Bulky_Core.Interfaces;
using Bulky_Core.Validators.Factory;
using Bulky_Infrastructure;
using Bulky_Infrastructure.Contexts;
using Bulky_Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Configurations
{
    public static class ServiceConfigurations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IServiceContainer,ServiceContainer>();
            services.AddScoped<IValidatorFactory, ValidatorFactory>();

            return services;
        }

    }
}
