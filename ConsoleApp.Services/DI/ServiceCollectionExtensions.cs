using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Services.Interfaces;
using ConsoleApp.Services.Services.Cars;

namespace ConsoleApp.Services.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCarServices(this IServiceCollection services)
        {
            // Register transient and scoped implementations
            services.AddTransient<ICarTransient, TransientCar>();
            services.AddScoped<ICarScoped, ScopedCar>();

            // Also register a default ICar to demonstrate resolving concrete services
            services.AddTransient<ICar, TransientCar>();

            return services;
        }
    }
}
