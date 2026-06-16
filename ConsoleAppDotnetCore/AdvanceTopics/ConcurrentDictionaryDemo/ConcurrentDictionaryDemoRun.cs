using System.Collections.Concurrent;

namespace ConsoleAppDotnetCore.AdvanceTopics.ConcurrentDictionaryDemo
{
    internal class ConcurrentDictionaryDemoRun
    {
        public static async Task Run()
        {
            var users1 = new Dictionary<int, string>();

            Parallel.For(0, 10000, i =>
            {
                try
                {
                    users1[i] = $"User{i}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Dictionary | Error occured for user {i.ToString()}| Error-> {ex.Message}");
                }
            });

            var users2 = new ConcurrentDictionary<int, string>();

            Parallel.For(0, 100000, i =>
            {
                try
                {
                    users2[i] = $"User{i}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ConcurrentDictionary | Error occured for user {i.ToString()}| Error-> {ex.Message}");
                }
            });

            users2 = new ConcurrentDictionary<int, string>();
            users2.TryAdd(1, "Ajay");

            Console.WriteLine("Execution Completed.");
        }
    }
}
