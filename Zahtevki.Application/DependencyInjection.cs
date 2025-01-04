using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Zahtevki.Application.Behaviours;

namespace Zahtevki.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
            return services;
        }
    }
}
