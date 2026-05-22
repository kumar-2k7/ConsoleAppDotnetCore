using Microsoft.Extensions.DependencyInjection;
using static ConsoleAppDotnetCore.FactoryMethodDemo.FactoryMethodDemoRun;

namespace ConsoleAppDotnetCore
{
    public static class DependencyMapping
    {
        public static IServiceCollection AddDependencyMapping(this IServiceCollection services)
        {
            services.AddSingleton<INotificationFactory, NotificationFactory>();

            //services.AddScoped<ICarScoped, ScopedCar>();

            //services.AddTransient<ICar, TransientCar>();

            return services;
        }
    }
}
