using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.Services;
using Persistance.TokenService;

namespace Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OzkaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICitiesReadRepository, CitiesReadRepository>();
            services.AddScoped<ICitiesWriteRepository, CitiesWriteRepository>();
            services.AddScoped<IJWTTokenServices, JWTTokenService>();

            return services;
        }
    }
}
