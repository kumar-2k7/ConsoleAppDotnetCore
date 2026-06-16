namespace ConsoleAppDotnetCore.AdvanceTopics.FastLookupDemo
{
    public class FastLookupDemoRun
    {
        public static void Run()
        {
            var employees = GetEmployeesLis();
            List<int> targetIds = new List<int> { 100, 200, 300 };

            var employeeLookup = employees.ToDictionary(e => e.Id, e => e.Name);

            Console.WriteLine($"Name: {employeeLookup[3]}");

            List<Employee> EmpList =
                [new Employee { Id = 1, Name = "Ajay" }
                ,new Employee { Id = 2, Name = "Ajay" }
                ,new Employee { Id = 3, Name = "Ajay" }];

            Parallel.ForEach(EmpList, e =>
            {
                try
                {
                    Console.WriteLine($"Employee Id: {e.Id}\tEmployee Name: {e.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private static List<Employee> GetEmployeesLis()
        {
            List<Employee> employees = new List<Employee>();
            string name = string.Empty;
            var random = new Random();
            var iterations = Enumerable.Range(1, 100000);

            string consonants = "bcdfghjklmnpqrstvwxyz";
            string vowels = "aeiou";

            for (int id = 1; id <= 100000; id++)
            {
                int length = random.Next(4, 9); // Length between 4 and 8
                char[] nameArray = new char[length];

                for (int i = 0; i < length; i++)
                {
                    // Alternate vowels and consonants for readability
                    if (i % 2 == 0)
                    {
                        nameArray[i] = consonants[random.Next(consonants.Length)];
                    }
                    else
                    {
                        nameArray[i] = vowels[random.Next(vowels.Length)];
                    }
                }

                // Capitalize the first letter
                nameArray[0] = char.ToUpper(nameArray[0]);
                string generatedName = new string(nameArray);

                employees.Add(new Employee { Id = id, Name = generatedName });
            }
            return employees;
        }

        class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }
    }
}
