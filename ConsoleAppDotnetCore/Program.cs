using ConsoleAppDotnetCore.FactoryMethodDemo;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDotnetCore
{
    internal class Program
    {

        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddDependencyMapping();

            //SingletonClassTestRun.Run();

            //AbstructionDemoRun.Run();

            //GenerateCsvFileDemoRun.Run();

            //ServiceLifeCycleDemoRun.Run();

            FactoryMethodDemoRun.Run(services);

            Console.ReadLine();
        }
    }
}
