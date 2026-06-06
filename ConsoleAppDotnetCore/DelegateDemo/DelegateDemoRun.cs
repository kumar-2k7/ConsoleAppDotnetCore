namespace ConsoleAppDotnetCore.DelegateDemo
{
    internal class DelegateDemoRun
    {
        public static void Run()
        {
            int num1 = 10, num2 = 11;
            MathOperation operation = Add;
            operation += Multiply;
            operation += Subtract;

            operation(30, 20);

            Notify notify = SendEmail;
            notify += SendSms;

            notify();

            Console.WriteLine($"private static Func<int, int, int> add = (a, b) => a + b;\n Answer => {add(num1, num2)}");
            printf("Ajay");
            Console.WriteLine(isEven(num1));
            Console.WriteLine(isEven(num2));
        }

        static Predicate<int> isEven = x => x % 2 == 0;

        static Action<string> printf = name => Console.WriteLine(name);

        private static Func<int, int, int> add = (a, b) => a + b;

        public delegate int MathOperation(int a, int b);

        public static int Add(int a, int b)
        {
            Console.WriteLine($"Addition of {a} and {b} is {a + b}");
            return a + b;
        }

        public static int Subtract(int a, int b)
        {
            Console.WriteLine($"Subtraction of {a} and {b} is {a - b}");
            return a - b;
        }

        public static int Multiply(int a, int b)
        {
            Console.WriteLine($"Multiplication of {a} and {b} is {a * b}");
            return a * b;
        }

        public delegate void Notify();

        public static void SendEmail()
        {
            Console.WriteLine("Email Sent");
        }

        public static void SendSms()
        {
            Console.WriteLine("SMS Sent");
        }
    }
}
