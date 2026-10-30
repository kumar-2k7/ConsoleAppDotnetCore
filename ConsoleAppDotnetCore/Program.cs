using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDotnetCore
{
    internal class Program
    {

        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddDependencyMapping();
            services.TestClassesWithRunMethods("DecoratorPatternDemo");
        }
    }
}
