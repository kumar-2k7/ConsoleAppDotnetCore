namespace ConsoleAppDotnetCore.SingletonClassDemo
{
    internal class CodeOptimizationDemo
    {
        internal static void OriginalPrintDoc(int num1)
        {
            Console.WriteLine("Original PrintDoc:");
            int a = num1 + 1;
            int b = a + 2;
            int c = b + 3;

            Console.WriteLine($"Value of C = {c}");

            for (int i = 0; i < 2; i++)
            {
                a = a + 10;
                b = a + 21;
                c = b + 33;

                Console.WriteLine($"Value of C = {c}");
            }
        }

        internal static void PrintDoc(int num1)
        {
            Console.WriteLine(++num1 + 5);

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine((num1 += 10) + 54);
            }
        }

        internal static void PrintDocOptional(int num1)
        {
            num1 += 1;
            Console.WriteLine(num1 + 5);

            for (int i = 0; i < 2; i++)
            {
                num1 += 10;
                Console.WriteLine(num1 + 54);
            }
        }
    }
}
