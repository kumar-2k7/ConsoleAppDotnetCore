namespace ConsoleAppDotnetCore.AbstructionDemo
{
    internal class AbstructionDemoRun
    {
        public static void Run()
        {

            string userInput = string.Empty;
            BankAccount accountAjay = new BankAccount();
            do
            {
                Console.Write($"\n\nPress D to deposit, W to withdraw, R to check balance, Q to quit: ");
                userInput = Console.ReadKey().KeyChar.ToString().ToUpper();

                if (userInput == "D")
                    accountAjay.Deposit();
                else if (userInput == "W")
                    accountAjay.Withdraw();
                else if (userInput == "R")
                    accountAjay.CheckBalance();

            } while (userInput != "Q");

            //SingletonClassTestRun.Run();

            Console.ReadLine();
        }

        public class BankAccount
        {
            // Private fields: Hidden from direct modification outside this class
            private decimal _balance;
            private string _name = string.Empty;

            public BankAccount()
            {
                Console.Write($"\n\nEnter account holder's name: ");
                _name = Console.ReadLine() ?? "";
                _balance = 0;
                Console.WriteLine($"BankAccount created for {_name} with initial balance: {_balance}");
            }

            // Public Method: The only way to modify internal state
            public void Deposit()
            {
                Console.WriteLine($"\n\nCurrent Balance before deposit: {_balance}");

                Console.Write($"\nEnter amount to deposit: ");
                try
                {
                    decimal amount = Convert.ToDecimal(Console.ReadLine());

                    Console.WriteLine($"\nAttempting to deposit: {amount}");
                    if (amount > 0)
                    {
                        _balance += amount;
                    }
                    Console.WriteLine($"New Balance after deposit: {_balance}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input for deposit amount. Please enter a valid decimal number. Error: {ex.Message}");
                }
            }

            public void Withdraw()
            {
                Console.WriteLine($"\n\nCurrent Balance before withdrawal: {_balance}");

                Console.Write($"\nEnter amount to withdrawal: ");
                try
                {
                    decimal amount = Convert.ToDecimal(Console.ReadLine());

                    Console.WriteLine($"\nAttempting to withdrawal: {amount}");
                    if (amount > 0)
                    {
                        _balance -= amount;
                    }
                    Console.WriteLine($"New Balance after withdrawal: {_balance}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input for withdrawal amount. Please enter a valid decimal number. Error: {ex.Message}");
                }
            }

            public void CheckBalance()
            {
                Console.WriteLine($"\n\nCurrent Balance of {_name}: {_balance}");
            }
        }
    }
}
