namespace ConsoleAppDotnetCore.AdvanceTopics.DesignPatterns.ObserverPattern
{
    public class ObserverPatternDemo
    {
        public static void Run()
        {
            var ticker = new StockTicker();

            var traderA = new StockMonitor("Desk A");
            var traderB = new StockMonitor("Desk B");

            using (ticker.Subscribe(traderA))
            using (var subB = ticker.Subscribe(traderB))
            {
                ticker.Publish("MSFT", 412.50m);
                ticker.Publish("AAPL", 225.10m);

                // Explicitly unsubscribe Desk B early
                subB.Dispose();

                ticker.Publish("MSFT", 414.00m);
            } // Desk A disposed here

            ticker.Publish("AAPL", 226.00m); // No output (all unsubscribed)
            ticker.Complete();
        }

        // 1. Payload
        public record StockTick(string Symbol, decimal Price, DateTime Timestamp);

        // 2. Subject / Provider (IObservable<T>)
        public sealed class StockTicker : IObservable<StockTick>
        {
            private readonly List<IObserver<StockTick>> _observers = new();
            private readonly object _lock = new();

            public IDisposable Subscribe(IObserver<StockTick> observer)
            {
                lock (_lock)
                {
                    if (!_observers.Contains(observer))
                    {
                        _observers.Add(observer);
                    }
                }

                // Return an IDisposable to let the observer clean up registration
                return new Unsubscriber(_observers, observer, _lock);
            }

            public void Publish(string symbol, decimal price)
            {
                Console.WriteLine("111");
                var tick = new StockTick(symbol, price, DateTime.UtcNow);
                List<IObserver<StockTick>> snapshot;

                lock (_lock)
                {
                    snapshot = new List<IObserver<StockTick>>(_observers);
                }

                foreach (var observer in snapshot)
                {
                    try
                    {
                        Console.WriteLine("222");
                        observer.OnNext(tick);
                        Console.WriteLine("333");
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                    }
                }
                Console.WriteLine("444");
            }

            public void Complete()
            {
                List<IObserver<StockTick>> snapshot;
                lock (_lock)
                {
                    snapshot = new List<IObserver<StockTick>>(_observers);
                    _observers.Clear();
                }

                foreach (var observer in snapshot)
                {
                    observer.OnCompleted();
                }
            }

            private sealed class Unsubscriber : IDisposable
            {
                private readonly List<IObserver<StockTick>> _observers;
                private readonly IObserver<StockTick> _observer;
                private readonly object _lock;

                public Unsubscriber(List<IObserver<StockTick>> observers, IObserver<StockTick> observer, object @lock)
                {
                    _observers = observers;
                    _observer = observer;
                    _lock = @lock;
                }

                public void Dispose()
                {
                    lock (_lock)
                    {
                        _observers.Remove(_observer);
                    }
                }
            }
        }

        // 3. Concrete Observer (IObserver<T>)
        public sealed class StockMonitor : IObserver<StockTick>
        {
            private readonly string _name;

            public StockMonitor(string name) => _name = name;

            public void OnNext(StockTick value) =>
                Console.WriteLine($"[{_name}] {value.Symbol}: ${value.Price:F2} at {value.Timestamp:T}");

            public void OnError(Exception error) =>
                Console.WriteLine($"[{_name}] Error received: {error.Message}");

            public void OnCompleted() =>
                Console.WriteLine($"[{_name}] Stream completed.");
        }
    }
}
