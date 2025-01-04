using Zahtevki.Application;
using Zahtevki.Infrastructure;

namespace Zahtevki.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI();
            services.AddInfrastructureDI();

            return services;
        }
    }
}