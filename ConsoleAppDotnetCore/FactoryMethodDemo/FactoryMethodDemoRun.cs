using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDotnetCore.FactoryMethodDemo
{
    internal class FactoryMethodDemoRun
    {
        public static void Run(ServiceCollection services)
        {
            Console.WriteLine("Factory Method Demo");

            INotification notification = new EmailNotification();
            notification.Send("Hello via Email!");
            notification = new SmsNotification();
            notification.Send("Hello via SMS!");


            using (var provider = services.BuildServiceProvider())
            {
                var _notificationFactory = provider.GetRequiredService<INotificationFactory>();

                Console.Write("\nEnter E for Email, S for SMS, or P for Pager: ");
                string notificationTypeInput = Console.ReadKey().KeyChar.ToString().ToUpper();

                NotificationType notificationType = notificationTypeInput switch
                {
                    "E" => NotificationType.Email,
                    "S" => NotificationType.Sms,
                    "P" => NotificationType.Pager,
                    _ => throw new ArgumentException("Invalid input")
                };

                INotification notification2 = _notificationFactory.CreateNotification(notificationType);

                // 2. Execute business logic seamlessly
                notification2.Send("Hello via Factory Method!");
            }

            Console.WriteLine();
        }

        // The Product Interface
        public interface INotification
        {
            public void Send(string message);
        }

        // Concrete Products
        internal class EmailNotification : INotification
        {
            public void Send(string message) => Console.WriteLine($"\nEmail sent: {message}");
        }

        internal class SmsNotification : INotification
        {
            public void Send(string message) => Console.WriteLine($"\nSMS sent: {message}");
        }

        internal class PagerNotification : INotification
        {
            public void Send(string message) => Console.WriteLine($"\nPager sent: {message}");
        }

        public interface INotificationFactory
        {
            public INotification CreateNotification(NotificationType messagetype);
        }

        // The Factory
        public class NotificationFactory : INotificationFactory
        {
            public INotification CreateNotification(NotificationType type)
            {
                return type switch
                {
                    NotificationType.Email => new EmailNotification(),
                    NotificationType.Sms => new SmsNotification(),
                    NotificationType.Pager => new PagerNotification(),
                    _ => throw new ArgumentException("Unknown notification type")
                };
            }
        }

        public enum NotificationType
        {
            Email,
            Sms,
            Pager
        }
    }
}
