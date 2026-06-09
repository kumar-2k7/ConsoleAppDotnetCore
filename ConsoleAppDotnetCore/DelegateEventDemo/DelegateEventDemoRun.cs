namespace ConsoleAppDotnetCore.DelegateEventDemo
{
    /// <summary>
    /// In C#, delegates act as the blueprint (function pointer), 
    /// while events are the encapsulation wrappers that protect 
    /// the delegate from being cleared or improperly invoked 
    /// from outside the defining class.
    /// </summary>
    internal class DelegateEventDemoRun
    {
        public static void Run()
        {
            // Instantiate the publisher
            Stock techStock = new Stock("MSFT", 400.00m);

            // Instantiate subscribers
            MobileAlertService smsService = new MobileAlertService();
            DisplayDashboard dashboardService = new DisplayDashboard();

            // STEP 5: Wire up the event subscriptions using the += operator
            techStock.PriceChanged += smsService.OnStockPriceChanged;
            techStock.PriceChanged += dashboardService.UpdateGraph;

            // Trigger the event by modifying the property
            Console.WriteLine("--- First Price Change ---");
            techStock.Price = 415.50m;

            // Unsubscribe a listener using the -= operator
            techStock.PriceChanged -= smsService.OnStockPriceChanged;

            // Trigger the event again (only the dashboard will react)
            Console.WriteLine("\n--- Second Price Change ---");
            techStock.Price = 412.00m;

            Console.ReadLine();
        }

        // STEP 1: Define a delegate that specifies the method signature for the event
        public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

        // STEP 2: Create a Publisher class that owns and triggers the event
        internal class Stock
        {
            private decimal _price;
            public string Symbol { get; }

            // Declare the event using the custom delegate blueprint
            public event PriceChangedHandler PriceChanged;

            public Stock(string symbol, decimal initialPrice)
            {
                Symbol = symbol;
                _price = initialPrice;
            }

            public decimal Price
            {
                get => _price;
                set
                {
                    if (_price != value)
                    {
                        decimal oldPrice = _price;
                        _price = value;

                        // STEP 3: Raise the event (safe invocation checks if there are any subscribers)
                        OnPriceChanged(oldPrice, _price);
                    }
                }
            }

            // Standard .NET pattern: a protected virtual method to raise the event safely
            protected virtual void OnPriceChanged(decimal oldPrice, decimal newPrice)
            {
                System.Threading.Thread.Sleep(3000); // Simulate work
                // The ?. operator ensures we only invoke if someone has subscribed
                PriceChanged?.Invoke(oldPrice, newPrice);
            }
        }

        #region STEP 4: Create Subscriber classes to react to the event
        internal class MobileAlertService
        {
            public void OnStockPriceChanged(decimal oldPrice, decimal newPrice)
            {
                Console.WriteLine($"[SMS Alert]: Price shifted from ${oldPrice} to ${newPrice}!");
            }
        }

        internal class DisplayDashboard
        {
            public void UpdateGraph(decimal oldPrice, decimal newPrice)
            {
                Console.WriteLine($"[UI Dashboard]: Graph updated. Trend: {(newPrice > oldPrice ? "UP by " + (newPrice - oldPrice).ToString() : "DOWN by " + (oldPrice - newPrice).ToString())}");
            }
        }
        #endregion
    }
}
