namespace ConsoleAppDotnetCore.SingletonClassDemo
{
    /// <summary>
    /// This class demonstrates the implementation of a singleton class and code optimization techniques in C#.
    /// </summary>
    internal class SingletonClassTestRun
    {
        /// <summary>
        /// Demonstrates the usage of a singleton class and code optimization techniques.
        /// </summary>
        public static void Run()
        {
            SingletonClassDemo.PrintInstance.PrintPaper();

            // Class constructor is inaccessible due to its protection level.
            // The following line will cause a compile-time error if uncommented.
            // SingletonClassDemo demo = new SingletonClassDemo();

            CodeOptimizationDemo.OriginalPrintDoc(10);
            CodeOptimizationDemo.PrintDoc(10);
            CodeOptimizationDemo.PrintDocOptional(10);


            Printer.PrintingService.PrintPaper();

            for (int i = 1; i < 10; i++)
            {
                Printer.PrintingService.PrintPaper();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// This class implements the singleton design pattern, ensuring that only one instance of the class exists throughout the application.
        /// </summary>
        class SingletonClassDemo
        {
            /// <summary>
            /// The Lazy<T> type ensures that the instance is created only when it is first accessed, and it is thread-safe by default.
            /// </summary>
            public static readonly Lazy<SingletonClassDemo> _printInstance = new Lazy<SingletonClassDemo>(() => new SingletonClassDemo());
            /// <summary>
            /// Provides a global point of access to the single instance of the SingletonClassDemo class.
            /// </summary>
            public static SingletonClassDemo PrintInstance => _printInstance.Value;
            /// <summary>
            /// The private constructor prevents external instantiation of the class, ensuring that only one instance can be created through the Lazy<T> mechanism.
            /// </summary>
            private SingletonClassDemo()
            {
            }
            /// <summary>
            /// A method that simulates a service provided by the singleton class, in this case, printing paper.
            /// </summary>
            public void PrintPaper()
            {
                Console.WriteLine("Print Paper Service");
            }
        }
        public sealed class Printer
        {
            private static readonly Printer _printingService = new Printer();
            private int printCount = 0;

            public static Printer PrintingService { get { return _printingService; } }

            private Printer()
            {
                printCount++;
            }

            public void PrintPaper()
            {
                Console.WriteLine($"Printing paper...{printCount++}");
            }
        }
    }
}
