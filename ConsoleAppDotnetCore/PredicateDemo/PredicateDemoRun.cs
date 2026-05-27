namespace ConsoleAppDotnetCore.PredicateDemo
{
    internal class PredicateDemoRun
    {
        public static void Run()
        {
            var users = new List<User>
            {
                new User { Name = "Aditya", Age = 3 },
                new User { Name = "Alice", Age = 25 },
                new User { Name = "Bob", Age = 15 },
                new User { Name = "Charlie", Age = 30 }
            };

            users.ForEach(adult => Console.WriteLine(adult.ToString()));
            Console.WriteLine("----");

            // Approach A: Using a traditional method pointer
            Predicate<User> traditionalPredicate = IsAdult;

            List<User> firstAdult = users.FindAll(traditionalPredicate);

            // Approach B: Inline Lambda Expression (The industry standard)
            // Read as: "Given a user 'u', return true if u.Age is greater than or equal to 18"
            List<User> allAdults = users.FindAll(u => u.Age >= 18);

            // Approach C: Destructive filtering
            users.RemoveAll(u => u.Age < 18); // Modifies the list in-place

            //Console.WriteLine(firstAdult.ToString());
            firstAdult.ForEach(adult => Console.WriteLine(adult.ToString()));
            Console.WriteLine("----");
            allAdults.ForEach(adult => Console.WriteLine(adult.ToString()));
            Console.WriteLine("----");
            users.ForEach(adult => Console.WriteLine(adult.ToString()));
            Console.WriteLine("----");

            Console.ReadKey();
        }

        // Target method used for Approach A
        private static bool IsAdult(User user)
        {
            return user.Age >= 18;
        }

        public class User
        {
            public string Name { get; set; } = default!;
            public int Age { get; set; }


            // Overriding the default ToString()
            public override string ToString()
            {
                return $"Name: {Name}, Age: {Age}";
            }
        }
    }
}
