using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppDotnetCore.AdvanceTopics.DesignPatterns
{
    internal class StrategyPatternDemo
    {
        internal static void Run()
        {
            // Initialize with Credit Card
            var processor = new OrderProcessor(new CreditCardPayment());
            processor.CompleteOrder(150.00m);

            // Dynamically change behavior to PayPal at runtime
            processor.SetPaymentStrategy(new PayPalPayment());
            processor.CompleteOrder(45.50m);
        }

        interface IPaymentStrategy
        {
            void Pay(decimal amount);
        }

        class CreditCardPayment : IPaymentStrategy
        {
            public void Pay(decimal amount)
            {
                Console.WriteLine($"Paid ${amount} using Credit Card.");
            }
        }

        class PayPalPayment : IPaymentStrategy
        {
            public void Pay(decimal amount)
            {
                Console.WriteLine($"Paid ${amount} using PayPal.");
            }
        }

        class OrderProcessor
        {
            private IPaymentStrategy _paymentStrategy;

            // Set the strategy via constructor
            public OrderProcessor(IPaymentStrategy paymentStrategy)
            {
                _paymentStrategy = paymentStrategy;
            }

            // Or allow swapping the strategy dynamically at runtime
            public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
            {
                _paymentStrategy = paymentStrategy;
            }

            public void CompleteOrder(decimal totalAmount)
            {
                // Delegation: Context delegates work to the strategy
                _paymentStrategy.Pay(totalAmount);
            }
        }
    }
}
