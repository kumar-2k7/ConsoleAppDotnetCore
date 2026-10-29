using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDotnetCore
{
    /// <summary>
    /// This static class contains an extension method for the ServiceCollection class that allows you to automatically find and execute public static 'Run' methods in classes within the same assembly. It is designed to facilitate testing and demonstration of different classes by specifying the class name you want to run.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// This extension method searches for all classes in the assembly that contain a public static 'Run' method. It then<br/>
        /// executes the 'Run' method of the specified class name. If the 'Run' method requires parameters, it will provide<br/>
        /// the ServiceProvider as an argument.
        /// </summary>
        /// <param name="ClassName">Name of the Class which has Run Method.</param>
        public static void TestClassesWithRunMethods(this ServiceCollection services, string ClassName)
        {

            // 1. Get all classes in this assembly that contain a public static 'Run' method
            var runnableMethods = typeof(Program).Assembly.GetTypes()
                .Select(t => t.GetMethod("Run", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                .Where(m => m != null)
                .ToList();

            // 2. Automatically execute the one you are currently working on!
            // Simply type the name of the topic class you want to test here:
            string activeDemoName = ClassName;

            var targetMethod = runnableMethods.FirstOrDefault(m => m!.DeclaringType!.Name == activeDemoName);

            if (targetMethod != null)
            {
                var parameters = targetMethod.GetParameters();

                if (parameters.Length == 0)
                {
                    // Method takes no parameters -> Run()
                    targetMethod.Invoke(null, null);
                }
                // If the method requires your ServiceProvider parameter (like FactoryMethodDemo)
                //else if (targetMethod.GetParameters().Length > 0)
                //{
                //    var provider = services.BuildServiceProvider();
                //    targetMethod.Invoke(null, new object[] { provider });
                //}
                else
                {
                    // Build argument array dynamically based on what the method actually expects
                    var args = new object[parameters.Length];
                    IServiceProvider? provider = null;

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;

                        if (paramType == typeof(ServiceCollection))
                        {
                            // Passes the raw ServiceCollection instance
                            args[i] = services;
                        }
                        else if (paramType == typeof(IServiceProvider))
                        {
                            // Lazily builds and passes the ServiceProvider
                            provider ??= services.BuildServiceProvider();
                            args[i] = provider;
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                $"Unsupported parameter type '{paramType.Name}' in {targetMethod.DeclaringType!.Name}.Run().");
                        }
                    }

                    targetMethod.Invoke(null, args);
                }
            }
            else
            {
                Console.WriteLine($"Demo class '{activeDemoName}' was not found.");
            }

            Console.ReadLine();
        }
    }
}
