using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zahtevki.Core.Interfaces;
using Zahtevki.Infrastructure.Data;
using Zahtevki.Infrastructure.Repositories;

namespace Zahtevki.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TasksDb");
            });

            services.AddScoped<ITasksRepository, TasksRepository>();

            return services;
        }
    }
}