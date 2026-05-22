namespace ConsoleAppDotnetCore.SingletonClassDemo
{
    internal class SingletonClassTestRun
    {
        public static void Run()
        {
            SingletonClassDemo.PrintInstance.PrintPaper();

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

        class SingletonClassDemo
        {

            public static readonly Lazy<SingletonClassDemo> _printInstance = new Lazy<SingletonClassDemo>(() => new SingletonClassDemo());

            public static SingletonClassDemo PrintInstance => _printInstance.Value;

            private SingletonClassDemo()
            {
            }

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
