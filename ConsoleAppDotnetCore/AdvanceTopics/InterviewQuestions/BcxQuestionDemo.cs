using System.Collections.Concurrent;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;

namespace ConsoleAppDotnetCore.AdvanceTopics.InterviewQuestions
{
    public class BcxQuestionDemo
    {
        public static void Run()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("BCX Question Demo");
            Console.WriteLine("This is a demo for BCX interview questions.");


            // Set up OpenTelemetry tracing with console exporter
            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource("ConsoleAppDotnetCore.AdvanceTopics.InterviewQuestions.OrderService")
                .AddConsoleExporter()
                .Build();

            var service = new OrderService();
            var order = service.CreateOrder();

            Console.WriteLine("Adding lines to order...");
            service.AddLine(order.OrderNo, "SKU123", 2, 10.99m, 10m);
            service.AddLine(order.OrderNo, "SKU456", 1, 20.00m, 5m);

            Console.WriteLine($"Order No: {order.OrderNo}");
            Console.WriteLine($"Order Created: {order.Created}");
            Console.WriteLine("Order Lines:");

            foreach (var line in order.Lines)
                Console.WriteLine($"  Line {line.LineNo}: {line.SKU} - Qty: {line.Qty}, Unit Price: {line.UnitPriceExVat:C}, Discount: {line.DiscountPercent}%");

            Console.WriteLine($"Order Total Ex VAT: {service.GetTotalExVat(order.OrderNo):C}");
            Console.WriteLine($"Order Total Inc VAT: {service.GetDiscountPercent(order.OrderNo):C}");
            Console.WriteLine("===============================================");
        }
    }

    public class OrderLine
    {
        public int LineNo { get; init; }
        public required string SKU { get; init; }
        public int Qty { get; init; }
        public decimal UnitPriceExVat { get; init; }
        public decimal DiscountPercent { get; init; }

        public decimal LineTotalExVat => Qty * UnitPriceExVat * (1 - DiscountPercent / 100);
    }

    public class Order
    {
        public Guid OrderNo { get; init; } = Guid.NewGuid();
        public DateTime Created { get; init; } = DateTime.UtcNow;

        // Internal backing list for order lines with thread-safe access
        private readonly List<OrderLine> _lines = [];

        // Expose as ReadOnly Collection to prevent external modification while ensuring thread safety
        public IReadOnlyCollection<OrderLine> Lines
        {
            get
            {
                lock (_lines)
                {
                    return _lines.ToList();
                }
            }
        }

        public void AddLine(string sku, int qty, decimal unitPriceExVat, decimal discountPercent)
        {
            lock (_lines)
            {
                var lineNo = _lines.Count + 1;
                var orderLine = new OrderLine
                {
                    LineNo = lineNo,
                    SKU = sku,
                    Qty = qty,
                    UnitPriceExVat = unitPriceExVat,
                    DiscountPercent = discountPercent
                };
                _lines.Add(orderLine);
            }
        }
    }

    public class OrderService
    {
        // High-Performance thread-safe In-Memory Storage for Orders
        private static readonly ConcurrentDictionary<Guid, Order> Orders = new ConcurrentDictionary<Guid, Order>();

        // OpenTelemetry ActivitySource for tracing
        private static readonly ActivitySource orderServiceSource = new("ConsoleAppDotnetCore.AdvanceTopics.InterviewQuestions.OrderService");

        public Order CreateOrder()
        {
            using Activity activity = orderServiceSource.StartActivity("OrderCreated")!;

            var o = new Order();
            Orders[o.OrderNo] = o;

            if (activity != null)
                activity.SetTag("Order.OrderNo", o.OrderNo.ToString());

            activity?.SetStatus(ActivityStatusCode.Ok, "Order created successfully.");

            return o;
        }

        public void AddLine(Guid orderNo, string sku, int qty, decimal unitPriceExVat, decimal discountPercent)
        {
            // OpenTelemetry Activity for tracing the AddLine operation
            using Activity activity = orderServiceSource.StartActivity("AddLine")!;

            if (activity != null)
            {
                activity.SetTag("Order.OrderNo", orderNo.ToString());
                activity.SetTag("Order.Line.SKU", sku);
                activity.SetTag("Order.Line.Qty", qty);
            }

            // Robust input validation rules with OpenTelemetry error reporting
            if (qty <= 0)
            {
                activity?.SetStatus(ActivityStatusCode.Error, "Quantity must be greater than zero..");
                throw new ArgumentException("Quantity must be greater than zero.", nameof(qty));
            }
            if (unitPriceExVat < 0)
            {
                activity?.SetStatus(ActivityStatusCode.Error, "Unit price must be non-negative..");
                throw new ArgumentException("Unit price must be non-negative.", nameof(unitPriceExVat));
            }
            if (discountPercent is < 0m or > 100m)
            {
                activity?.SetStatus(ActivityStatusCode.Error, "Discount percent must be between 0 and 100..");
                throw new ArgumentException("Discount percent must be between 0 and 100.", nameof(discountPercent));
            }

            // Handle Order missing scenario cleanly without crashing with OpenTelemetry error reporting
            if (!Orders.TryGetValue(orderNo, out var order))
            {
                activity?.SetStatus(ActivityStatusCode.Error, "Order not found..");
                throw new KeyNotFoundException($"Order with OrderNo {orderNo} not found.");
            }

            order.AddLine(sku, qty, unitPriceExVat, discountPercent);
            activity?.SetStatus(ActivityStatusCode.Ok, "Line added successfully..");
        }

        public decimal GetTotalExVat(Guid orderNo)
        {
            if (!Orders.TryGetValue(orderNo, out var order))
                return 0m;

            // Calculate sum while accurately accounting for applied discounts and ensuring thread safety
            return order.Lines.Sum(line => line.LineTotalExVat);
        }

        public decimal GetDiscountPercent(Guid orderNo)
        {
            const decimal VatRate = 0.15m;  // Clearer extraction of rates
            var totalEx = GetTotalExVat(orderNo);
            return totalEx * (1 + VatRate);
        }
    }
}
