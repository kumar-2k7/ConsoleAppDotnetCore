namespace ConsoleAppDotnetCore.YieldKeywordDemo
{
    /// <summary>
    /// The yield keyword in C# is used to create an iterator method that ⁠<br/>
    /// customizes data sequence generation on demand. It allows a method to <br/>
    /// return elements to a loop one at a time without creating or caching <br/>
    /// a temporary collection in memory.<br/><br/>
    /// When the compiler encounters yield, it automatically generates a state <br/>
    /// machine that tracks the exact location of the code execution and preserves <br/>
    /// local variables across subsequent iteration cycles.
    /// </summary>
    internal class YieldKeywordDemoRun
    {
        public static void Run()
        {
            Console.WriteLine("\nConsuming the iterator using a foreach loop normally:\n");
            // Consuming the iterator using a foreach loop
            foreach (int number in GetEvenNumbers(100))
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nConsuming the iterator using a foreach loop with yield:\n");
            // Consuming the iterator using a foreach loop
            foreach (int number in GetEvenNumbersWithYield(100))
            {
                Console.WriteLine(number);
            }
        }

        static IEnumerable<int> GetEvenNumbers(int max)
        {
            var list = new List<int>();
            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    Thread.Sleep(100);
                    list.Add(i); // Suspends execution here and returns value
                }
            }
            return list;
        }

        // Iterator method utilizing yield return
        static IEnumerable<int> GetEvenNumbersWithYield(int max)
        {
            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    Thread.Sleep(100);
                    yield return i; // Suspends execution here and returns value
                }
            }
        }
    }
}
