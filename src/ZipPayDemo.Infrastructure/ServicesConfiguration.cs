using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ZipPayDemo.Infrastructure.Contracts;
using ZipPayDemo.Infrastructure.Services;

namespace ZipPayDemo.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
