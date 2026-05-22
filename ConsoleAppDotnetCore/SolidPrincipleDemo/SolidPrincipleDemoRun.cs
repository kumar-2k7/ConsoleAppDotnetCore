using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppDotnetCore.SolidPrincipleDemo;

public static class SolidPrincipleDemoRun
{
    public static void Run()
    {
        Console.WriteLine("SOLID Principles Demo\n");

        SingleResponsibilityPrincipleDemo();
        OpenClosedPrincipleDemo();
        LiskovSubstitutionPrincipleDemo();
        InterfaceSegregationPrincipleDemo();
        DependencyInversionPrincipleDemo();

        Console.WriteLine();
    }

    // -----------------------
    // 1) Single Responsibility Principle (SRP)
    // A class should have only one reason to change. Keep responsibilities separate.
    // Example (E-commerce): Invoice is a data holder; calculation and persistence are separate responsibilities.
    private static void SingleResponsibilityPrincipleDemo()
    {
        Console.WriteLine("1) Single Responsibility Principle");

        var invoice = new Invoice { Items = new List<InvoiceItem> { new(29.99m, 2), new(15.50m, 1) } };

        // InvoiceCalculator is responsible for computing totals
        var calculator = new InvoiceCalculator();
        var total = calculator.CalculateTotal(invoice);

        // InvoicePrinter is responsible for presentation
        var printer = new InvoicePrinter();
        printer.Print(invoice, total);

        Console.WriteLine();
    }

    // -----------------------
    // 2) Open/Closed Principle (OCP)
    // Software entities should be open for extension but closed for modification.
    // Example (Payments): PaymentService can accept new payment processors without modification.
    private static void OpenClosedPrincipleDemo()
    {
        Console.WriteLine("2) Open/Closed Principle");

        var orderAmount = 99.95m;

        IPaymentProcessor creditCard = new CreditCardProcessor("4111-....");
        IPaymentProcessor payPal = new PayPalProcessor("merchant@shop.com");

        var paymentService = new PaymentService();
        paymentService.ProcessPayment(orderAmount, creditCard);
        paymentService.ProcessPayment(orderAmount, payPal);

        // Later we can add a new processor (e.g., ApplePayProcessor) without changing PaymentService

        Console.WriteLine();
    }

    // -----------------------
    // 3) Liskov Substitution Principle (LSP)
    // Subtypes must be substitutable for their base types without altering desirable properties.
    // Example (Banking): Any Account implementation must obey withdraw semantics (no unexpected exceptions or extra preconditions).
    private static void LiskovSubstitutionPrincipleDemo()
    {
        Console.WriteLine("3) Liskov Substitution Principle");

        Account savings = new SavingsAccount(1000m);
        Account checking = new CheckingAccount(100m, overdraftLimit: 200m);

        ProcessWithdrawal(savings, 200m);
        ProcessWithdrawal(checking, 250m); // allowed due to overdraft

        Console.WriteLine();
    }

    private static void ProcessWithdrawal(Account account, decimal amount)
    {
        Console.WriteLine($"Attempting to withdraw {amount:C} from {account.GetType().Name} (balance: {account.Balance:C})");
        if (account.TryWithdraw(amount))
            Console.WriteLine("Withdrawal succeeded.");
        else
            Console.WriteLine("Withdrawal failed: insufficient funds or limit reached.");
    }

    // -----------------------
    // 4) Interface Segregation Principle (ISP)
    // Clients should not be forced to depend on interfaces they do not use.
    // Example (Notifications): Separate email and SMS concerns so implementations only implement what they need.
    private static void InterfaceSegregationPrincipleDemo()
    {
        Console.WriteLine("4) Interface Segregation Principle");

        IEmailSender emailSender = new SmtpEmailSender();
        ISmsSender smsSender = new TwilioSmsSender();

        var emailController = new EmailNotificationController(emailSender);
        var smsController = new SmsNotificationController(smsSender);

        emailController.SendOrderConfirmation("customer@shop.com", "Order #12345");
        smsController.SendDeliveryUpdate("+15551234567", "Your package is out for delivery");

        Console.WriteLine();
    }

    // -----------------------
    // 5) Dependency Inversion Principle (DIP)
    // High-level modules should not depend on low-level modules; both should depend on abstractions.
    // Example (E-commerce): OrderProcessor depends on IOrderRepository and INotificationService abstractions.
    private static void DependencyInversionPrincipleDemo()
    {
        Console.WriteLine("5) Dependency Inversion Principle");

        IOrderRepository repo = new InMemoryOrderRepository();
        INotificationService notifier = new ConsoleNotificationService();

        var orderProcessor = new OrderProcessor(repo, notifier);

        var order = new Order(Guid.NewGuid(), 3, 49.99m);
        orderProcessor.PlaceOrder(order);

        Console.WriteLine();
    }
}

// -----------------------
// SRP types

public record InvoiceItem(decimal Price, int Quantity);

public class Invoice
{
    public List<InvoiceItem> Items { get; init; } = new();
}

public class InvoiceCalculator
{
    public decimal CalculateTotal(Invoice invoice) => invoice.Items.Sum(i => i.Price * i.Quantity);
}

public class InvoicePrinter
{
    public void Print(Invoice invoice, decimal total)
    {
        Console.WriteLine("Invoice:");
        foreach (var it in invoice.Items)
            Console.WriteLine($" - {it.Quantity} x {it.Price:C} = {it.Price * it.Quantity:C}");
        Console.WriteLine($"Total: {total:C}");
    }

    // Printer responsibility is separate from calculation or persistence
}

// -----------------------
// OCP types

public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount);
}

public class CreditCardProcessor : IPaymentProcessor
{
    private readonly string _cardNumber;
    public CreditCardProcessor(string cardNumber) => _cardNumber = cardNumber;
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Charging {amount:C} to credit card {_cardNumber}...");
        return true; // simulate success
    }
}

public class PayPalProcessor : IPaymentProcessor
{
    private readonly string _merchantEmail;
    public PayPalProcessor(string merchantEmail) => _merchantEmail = merchantEmail;
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing {amount:C} via PayPal account {_merchantEmail}...");
        return true; // simulate success
    }
}

public class PaymentService
{
    // PaymentService works against IPaymentProcessor abstraction; new processors can be added without modifying this class
    public void ProcessPayment(decimal amount, IPaymentProcessor processor)
    {
        var ok = processor.ProcessPayment(amount);
        Console.WriteLine(ok ? "Payment processed." : "Payment failed.");
    }
}

// -----------------------
// LSP types

public abstract class Account
{
    public decimal Balance { get; protected set; }
    public Account(decimal startingBalance) => Balance = startingBalance;

    // TryWithdraw expresses a safe contract: attempt to withdraw and return success/failure
    public abstract bool TryWithdraw(decimal amount);
}

public class SavingsAccount : Account
{
    public SavingsAccount(decimal startingBalance) : base(startingBalance) { }

    public override bool TryWithdraw(decimal amount)
    {
        if (amount <= 0) return false;
        if (amount > Balance) return false; // no overdraft
        Balance -= amount;
        return true;
    }
}

public class CheckingAccount : Account
{
    private readonly decimal _overdraftLimit;
    public CheckingAccount(decimal startingBalance, decimal overdraftLimit) : base(startingBalance) => _overdraftLimit = overdraftLimit;

    public override bool TryWithdraw(decimal amount)
    {
        if (amount <= 0) return false;
        if (amount > Balance + _overdraftLimit) return false; // respect overdraft limit
        Balance -= amount;
        return true;
    }
}

// -----------------------
// ISP types

public interface IEmailSender { void SendEmail(string to, string subject, string body); }
public interface ISmsSender { void SendSms(string number, string message); }

public class SmtpEmailSender : IEmailSender
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending Email to {to}: {subject} - {body}");
    }
}

public class TwilioSmsSender : ISmsSender
{
    public void SendSms(string number, string message)
    {
        Console.WriteLine($"Sending SMS to {number}: {message}");
    }
}

public class EmailNotificationController
{
    private readonly IEmailSender _emailSender;
    public EmailNotificationController(IEmailSender emailSender) => _emailSender = emailSender;
    public void SendOrderConfirmation(string customerEmail, string orderRef) => _emailSender.SendEmail(customerEmail, "Order Confirmation", orderRef);
}

public class SmsNotificationController
{
    private readonly ISmsSender _smsSender;
    public SmsNotificationController(ISmsSender smsSender) => _smsSender = smsSender;
    public void SendDeliveryUpdate(string phone, string message) => _smsSender.SendSms(phone, message);
}

// -----------------------
// DIP types

public record Order(Guid Id, int Quantity, decimal UnitPrice);

public interface IOrderRepository
{
    void Save(Order order);
}

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    public void Save(Order order)
    {
        _orders.Add(order);
        Console.WriteLine($"Order {order.Id} saved (qty: {order.Quantity}, total: {order.Quantity * order.UnitPrice:C})");
    }
}

public interface INotificationService { void Notify(string recipient, string message); }

public class ConsoleNotificationService : INotificationService
{
    public void Notify(string recipient, string message) => Console.WriteLine($"Notify {recipient}: {message}");
}

public class OrderProcessor
{
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notification;

    public OrderProcessor(IOrderRepository repository, INotificationService notification)
    {
        _repository = repository;
        _notification = notification;
    }

    public void PlaceOrder(Order order)
    {
        _repository.Save(order);
        _notification.Notify("warehouse@shop.com", $"Order placed: {order.Id}");
    }
}

