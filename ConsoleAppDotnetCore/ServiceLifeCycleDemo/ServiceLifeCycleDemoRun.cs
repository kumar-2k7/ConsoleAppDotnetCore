using ConsoleApp.Services.DI;
using ConsoleApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDotnetCore.ServiceLifeCycleDemo
{
    internal class ServiceLifeCycleDemoRun
    {
        public static void Run()
        {
            Console.WriteLine("Service Life Cycle Demo is running...");
            // The actual demonstration of service lifetimes (Transient, Scoped, Singleton) is in Program.cs


            // Setup DI
            var services = new ServiceCollection();
            services.AddCarServices();

            using var provider = services.BuildServiceProvider();

            // Demonstrate transient: each resolve gives new instance
            var t1 = provider.GetRequiredService<ICarTransient>();
            var t2 = provider.GetRequiredService<ICarTransient>();

            Console.WriteLine($"Transient instances: {t1.InstanceId} vs {t2.InstanceId}");

            // Demonstrate scoped: within same scope same instance; across scopes different
            using (var scope1 = provider.CreateScope())
            {
                var s1a = scope1.ServiceProvider.GetRequiredService<ICarScoped>();
                var s1b = scope1.ServiceProvider.GetRequiredService<ICarScoped>();
                Console.WriteLine($"Scoped in scope1: {s1a.InstanceId} vs {s1b.InstanceId}");
            }

            using (var scope2 = provider.CreateScope())
            {
                var s2 = scope2.ServiceProvider.GetRequiredService<ICarScoped>();
                Console.WriteLine($"Scoped in scope2: {s2.InstanceId}");
            }
        }
    }
}
