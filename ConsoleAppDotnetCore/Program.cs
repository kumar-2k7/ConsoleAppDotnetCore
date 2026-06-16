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

            //DelegateDemo.DelegateDemoRun.Run();

            //PatternMatchingDemo.PatternMatchingDemoRun.Run();

            //EventDemo.EventDemoRun.Run();

            //DelegateEventDemo.DelegateEventDemoRun.Run();

            //PartialClassDemo.PartialClassDemoRun.Run();

            YieldKeywordDemo.YieldKeywordDemoRun.Run();

            //CancellationTokenDemo.CancellationTokenDemoRun.Run();

            //AdvanceTopics.ConcurrentDictionaryDemo.ConcurrentDictionaryDemoRun.Run().GetAwaiter().GetResult();

            //AdvanceTopics.FastLookupDemo.FastLookupDemoRun.Run();

            Console.ReadLine();
        }
    }
}
