using System.Diagnostics;

namespace ConsoleAppDotnetCore.SemaphoreSlimDemo
{
    internal class SemaphoreSlimDemoRun
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static async Task Run()
        {
            int totalRequests = 100_000;
            int maxConcurrency = 500; // Limit simultaneous connections

            Console.WriteLine($"Starting {totalRequests} requests with a concurrency cap of {maxConcurrency}...");
            var stopwatch = Stopwatch.StartNew();

            await ProcessRequestsAsync(totalRequests, maxConcurrency);

            stopwatch.Stop();
            Console.WriteLine($"Finished all requests in: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
        }

        private static async Task ProcessRequestsAsync(int totalRequests, int maxConcurrency)
        {// Semaphore limits the number of threads entering this code block simultaneously
            using var semaphore = new SemaphoreSlim(maxConcurrency);
            var tasks = new List<Task>();

            for (int i = 0; i < totalRequests; i++)
            {
                int requestId = i; // Capture loop variable

                // Asynchronously wait for a slot to open up in the semaphore
                await semaphore.WaitAsync();

                // Start the task and hand it over to the runtime
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        Console.WriteLine($"await ExecuteMockWebApiCall() started for request {requestId}");
                        await ExecuteMockWebApiCall(requestId);
                    }
                    finally
                    {
                        // CRITICAL: Always release the semaphore slot when done
                        semaphore.Release();
                        Console.WriteLine($"semaphore.Released for request {requestId}\n");
                    }
                }));
            }

            // Wait for all outstanding tasks to fully wrap up
            await Task.WhenAll(tasks);
            Console.WriteLine($"\nAll Tasks completed.");
        }

        private static async Task ExecuteMockWebApiCall(int requestId)
        {
            Console.WriteLine($"ExecuteMockWebApiCall() API called started for request {requestId}");
            // Simulating an outbound network request to an API endpoint
            // In production, replace with: await _httpClient.GetAsync($"https://api.example.com/data/{id}");
            await Task.Delay(100);
            Console.WriteLine($"ExecuteMockWebApiCall() API called completed for request {requestId}");
        }
    }
}
