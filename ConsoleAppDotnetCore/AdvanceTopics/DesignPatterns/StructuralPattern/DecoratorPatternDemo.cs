namespace ConsoleAppDotnetCore.AdvanceTopics.DesignPatterns.StructuralPattern
{
    internal class DecoratorPatternDemo
    {
        internal static void Run()
        {
            //  Wrap EmailService with both Logging and Encryption dynamiclly at runtime. The order of wrapping is important. LoggingDecorator is the outermost decorator, and EncryptionDecorator is the innermost decorator.
            IMessageService client = new LoggingDecorator(new EncryptionDecorator(new EmailService()));
            client.Send("Top Secret Data");
        }

        // 1. Define the interface for the service that will be decorated.
        public interface IMessageService
        {
            void Send(string message);
        }

        // 2. Implement the concrete service that will be decorated.
        public class EmailService : IMessageService
        {
            public void Send(string message) => Console.WriteLine($"EmailService: Sending message: {message}");
        }

        public abstract class MessageDecorator : IMessageService
        {
            protected readonly IMessageService _innerinnerService;
            protected MessageDecorator(IMessageService innerService) => _innerinnerService = innerService;
            public virtual void Send(string message) => _innerinnerService.Send(message);
        }

        public class EncryptionDecorator : MessageDecorator
        {
            public EncryptionDecorator(IMessageService innerService) : base(innerService) { }
            public override void Send(string message)
            {
                //string encryptedMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
                string encryptedMessage = $"[Encrypted] {message}";
                base.Send(encryptedMessage);
            }
        }

        // 3. Implement the decorators.
        private class LoggingDecorator : MessageDecorator
        {
            public LoggingDecorator(IMessageService innerService) : base(innerService) { }
            public override void Send(string message)
            {
                Console.WriteLine($"LOG: Preparing to send message...");
                base.Send(message);
            }
        }
    }
}
