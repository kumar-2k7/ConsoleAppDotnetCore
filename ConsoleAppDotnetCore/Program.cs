using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDotnetCore
{
    internal class Program
    {

        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddDependencyMapping();

            //SingletonClassDemo.SingletonClassTestRun.Run();

            //AbstructionDemo.AbstructionDemoRun.Run();

            //GenerateCsvFileDemo.GenerateCsvFileDemoRun.Run();

            //ServiceLifeCycleDemo.ServiceLifeCycleDemoRun.Run();

            //FactoryMethodDemo.FactoryMethodDemoRun.Run(services);

            //PredicateDemo.PredicateDemoRun.Run();

            //RepositoryPatternDemo.RepositoryPatternDemoRun.Run(services);

            //SemaphoreSlimDemo.SemaphoreSlimDemoRun.Run().GetAwaiter().GetResult();

            //TypeSystem.PrimitiveTypesDemo.Run();

            //TypeSystem.ReferenceTypeDemo.Run();

            DelegateDemo.DelegateDemoRun.Run();

            Console.ReadLine();
        }
    }
}
