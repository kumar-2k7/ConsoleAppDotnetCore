namespace ConsoleAppDotnetCore.CancellationTokenDemo
{
    /// <summary>
    /// A CancellationToken in C# is a lightweight struct used to enable<br/>
    /// cooperative cancellation between threads, thread pool work items, <br/>
    /// or asynchronous Task objects. Instead of forcefully terminating a <br/>
    /// thread - which can cause memory corruption or resource leaks - a token <br/>
    /// signals to a running operation that it should gracefully stop executing.
    /// </summary>
    internal class CancellationTokenDemoRun
    {
        public async static void Run()
        {
            // 1. Create the source (implements IDisposable, so use 'using')
            using var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            // 2. Start the asynchronous worker method
            Task workerTask = DoHeavyWorkAsync(token);

            // Simulate some app activity, then trigger a cancel after 1.5 seconds
            await Task.Delay(1500);
            Console.WriteLine("Requesting cancellation...");
            cts.Cancel();

            try
            {
                await workerTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was successfully canceled gracefully.");
            }
        }

        static async Task DoHeavyWorkAsync(CancellationToken token)
        {
            for (int i = 0; i < 10; i++)
            {
                // Throws OperationCanceledException immediately if cts.Cancel() was called
                token.ThrowIfCancellationRequested();

                Console.WriteLine($"Processing step {i}...");
                await Task.Delay(250, token);
            }
        }
    }
}
