using System.Data;
using System.Diagnostics;

namespace ConsoleAppDotnetCore.PatternMatchingDemo
{
    /// <summary>
    /// 1st Definition: Pattern Matching allows you to check a value's type, shape, 
    /// properties, and conditions in a concise way instead of 
    /// writing complex if-else statements.<br/>
    /// 2nd Definition: Pattern matching in C# is a powerful feature that allows
    /// you to test whether an expression has certain characteristics (like a 
    /// specific type, value, or structure) and safely extract data from it in 
    /// a single step.
    /// </summary>
    internal class PatternMatchingDemoRun
    {
        public static void Run()
        {
            TypePattern();
            SwitchExpression();
            PropertyPattern();
            RelationalPatterns();

            object vehicle = new Car { HorsePower = 300 };

            // Tests if 'vehicle' is a Car. If true, casts it to 'c' immediately.
            if (vehicle is Car c)
            {
                Console.WriteLine($"It's a car with {c.HorsePower} HP.");
            }

            Console.WriteLine("\nConstant Patterns:");
            Console.WriteLine($"GetStatusMessage(200): {GetStatusMessage(200)}");
            Console.WriteLine($"GetStatusMessage(404): {GetStatusMessage(404)}");
            Console.WriteLine($"GetStatusMessage(500): {GetStatusMessage(500)}");
            Console.WriteLine($"GetStatusMessage(201): {GetStatusMessage(201)}");
            Console.WriteLine($"GetStatusMessage(301): {GetStatusMessage(301)}");

            Console.WriteLine("\nRelational Patterns:");
            Console.WriteLine($"CategorizeTemperature(-15.5): {CategorizeTemperature(-15.5)}");
            Console.WriteLine($"CategorizeTemperature(280): {CategorizeTemperature(280)}");
            Console.WriteLine($"CategorizeTemperature(0): {CategorizeTemperature(0)}");

            Console.WriteLine("\nLogical Patterns:");
            Console.WriteLine($"IsValidDeliveryWeight(-5): {IsValidDeliveryWeight(-5)}");
            Console.WriteLine($"IsValidDeliveryWeight(0): {IsValidDeliveryWeight(0)}");
            Console.WriteLine($"IsValidDeliveryWeight(1): {IsValidDeliveryWeight(1)}");
            Console.WriteLine($"IsValidDeliveryWeight(50): {IsValidDeliveryWeight(50)}");
            Console.WriteLine($"IsValidDeliveryWeight(51): {IsValidDeliveryWeight(51)}");

            Console.WriteLine("\nProperty Patterns:");
            Console.WriteLine($"CalculateShipping(new Order(\"EU\", 150m)): {CalculateShipping(new Order("EU", 150m))}");
            Console.WriteLine($"CalculateShipping(new Order(\"US\", 150m)): {CalculateShipping(new Order("US", 150m))}");
            Console.WriteLine($"CalculateShipping(new Order(\"US\", 50m)): {CalculateShipping(new Order("US", 50m))}");
            Console.WriteLine($"CalculateShipping(new Order(\"IN\", 150m)): {CalculateShipping(new Order("IN", 150m))}");

            Console.WriteLine("\nPositional (Tuple) Patterns:");
            Console.WriteLine($"GetAccessTier(\"Admin\", true): {GetAccessTier("Admin", true)}");
            Console.WriteLine($"GetAccessTier(\"Admin\", false): {GetAccessTier("Admin", false)}");
            Console.WriteLine($"GetAccessTier(\"User\", true): {GetAccessTier("User", true)}");
            Console.WriteLine($"GetAccessTier(\"User\", false): {GetAccessTier("User", false)}");
            Console.WriteLine($"GetAccessTier(\"Guest\", true): {GetAccessTier("Guest", true)}");
            Console.WriteLine($"GetAccessTier(\"Guest\", false): {GetAccessTier("Guest", false)}");

            Console.WriteLine("\nList Patterns:");
            Console.WriteLine($"CheckList(new int[] {{ 1, 2, 3, 4, 5 }}): {CheckList(new int[] { 1, 2, 3, 4, 5 })}");
            Console.WriteLine($"CheckList(new int[] {{ 1, 2, 5 }}): {CheckList(new int[] { 1, 2, 5 })}");
            Console.WriteLine($"CheckList(new int[] {{ 1, 2, 3, 5 }}): {CheckList(new int[] { 1, 2, 3, 5 })}");
            Console.WriteLine($"CheckList(new int[] {{ 1, 2, 3, 4 }}): {CheckList(new int[] { 1, 2, 3, 4 })}");
            Console.WriteLine($"CheckList(new int[] {{ 2, 3, 4, 5 }}): {CheckList(new int[] { 2, 3, 4, 5 })}");
            Console.WriteLine($"CheckList(new int[] {{ 1, 2, 3, 4, 5, 6 }}): {CheckList(new int[] { 1, 2, 3, 4, 5, 6 })}");
            Console.WriteLine($"CheckList(new int[] {{ -1, 1, 2, 3, 4, 5, 6 }}): {CheckList(new int[] { -1, 1, 2, 3, 4, 5, 6 })}");
            Console.WriteLine($"CheckList(new int[] {{ 1, 2, -1, 3, 4, 6, 5 }}): {CheckList(new int[] { 1, 2, -1, 3, 4, 6, 5 })}");
        }


        /// <summary>
        /// Constant Patterns:<br></br>
        /// Tests whether an expression evaluates to a specific compile-time constant literal (null, true, numbers, or strings).
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static string GetStatusMessage(int statusCode) => statusCode switch
        {
            200 or 201 => "Success/Created",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Unknown Status Code" // The '_' discard acts as the default wildcard fallback
        };

        /// <summary>
        /// Relational Patterns:<br></br>
        /// Introduced in C# 9.0, this allows you to use relational operators (<, >, <=, >=) directly within switch expressions to evaluate numeric boundaries.
        /// </summary>
        /// <param name="celsius"></param>
        /// <returns></returns>
        public static string CategorizeTemperature(double celsius) => celsius switch
        {
            < 0 => "Freezing",
            >= 100 => "Boiling",
            _ => "Moderate"
        };

        /// <summary>
        /// Logical Patterns (Combinators):<br></br>
        /// Combines multiple individual patterns using the logical operators and, or, and not.
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static bool IsValidDeliveryWeight(double weight) => weight switch
        {
            > 0 and <= 50 => true,  // Must satisfy both conditions
            > 50 or < 0 => false, // Satisfies either condition
            _ => false
        };

        /// <summary>
        /// Property Patterns:<br></br>
        /// Matches fields or properties of an object against nested values or nested sub-patterns. This is highly effective for inspecting deeply nested object graphs.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static decimal CalculateShipping(Order order) => order switch
        {
            // Matches if the DeliveryRegion property equals "EU"
            { DeliveryRegion: "EU" } => 5.00m,

            // Matches if Region is "US" and the Cost property matches a relational pattern
            { DeliveryRegion: "US", Cost: > 100m } => 0.00m,
            { DeliveryRegion: "US" } => 15.00m,

            _ => 25.00m
        };

        /// <summary>
        /// Positional (Tuple) Patterns:<br></br>
        /// Deconstructs an object instance or a tuple value structure into discrete components and evaluates those positional positions simultaneously.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="IsTwoFactorEnabled"></param>
        /// <returns></returns>
        public static string GetAccessTier(string role, bool IsTwoFactorEnabled) => (role, IsTwoFactorEnabled) switch
        {
            ("Admin", true) => "Full System Access",
            ("Admin", false) => "Restricted - 2FA Required",
            ("User", _) => "Standard Customer Tier", // Discards the 2FA flag entirely
            _ => "Guest Access denied"
        };

        /// <summary>
        /// List Patterns:<br></br>
        /// Introduced in C# 11, list patterns allow you to match the explicit sequence of elements or array lengths inside collections, using slice expressions (..) to represent any number of middle variables.
        /// </summary>
        /// <param name="numberList"></param>
        /// <returns></returns>
        public static bool CheckList(int[] numberList)
        {
            if (numberList is [1, 2, .., 5])
                return true;

            return false;
        }

        private static void RelationalPatterns()
        {
            Console.WriteLine("\nRelational Patterns:");
            int age = 26;
            //  Traditional Switch
            Console.WriteLine($"Traditional Code Output: {"GetRole(2)"}");
            if (age >= 18)
            {
            }

            //  Relational Patterns
            Console.WriteLine($"\nRelational Patterns Output:\n");
            if (age is >= 18)
            {
            }
        }

        private static void PropertyPattern()
        {
            Console.WriteLine("\nProperty Pattern:");
            //  Traditional Switch
            Console.WriteLine($"Traditional Code Output: {"GetRole(2)"}");

            //  Property Pattern
            Console.WriteLine($"\nProperty Pattern Output:\n");
            List<Employee> employees = GetEmployees();
            Parallel.ForEach(employees, employee =>
            {
                if (employee is { Salary: > 50000 })
                    Console.WriteLine($"{employee.Name} has High Salary of {employee.Salary}.");
            });
        }

        private static List<Employee> GetEmployees()
        {
            return [
                new() {Name="Ajay", Salary=45000},
                new() {Name="Swati", Salary=45000},
                new() {Name="Atul", Salary=55000},
                new() {Name="Deepak", Salary=50000},
                new() {Name="Mukul", Salary=70000},
                new() {Name="Sanjeev", Salary=45000}
                ];
        }

        private static void SwitchExpression()
        {
            Console.WriteLine("\nSwitch Expression:");
            //  Traditional Switch
            string GetRole(int tRoleId)
            {
                switch (tRoleId)
                {
                    case 1:
                        return "Admin";
                    case 2:
                        return "Manager";
                    default:
                        return "User";
                }
            }
            Console.WriteLine($"Traditional Code Output: {GetRole(2)}");

            //  Switch Expression
            int seRoleId = 3;
            string role = seRoleId switch
            {
                1 => "Admin",
                2 => "Manager",
                _ => "User"
            };
            Console.WriteLine($"Switch Expression Output: {role}");
        }

        private static void TypePattern()
        {
            Console.WriteLine("\nType Pattern:");
            //  Traditional Code
            object value = 10;

            if (value is int)
            {
                int tNumber = (int)value;
                Console.WriteLine($"Traditional Code Output: {tNumber}");
            }

            //  Pattern Matching
            value = 10;

            if (value is int pmNumber)
            {
                Console.WriteLine($"Pattern Matching Output: {pmNumber}");
            }
        }

        private class Employee
        {
            public string Name { get; set; }
            public decimal Salary { get; set; }
        }

        private record Car
        {
            public int HorsePower { get; set; }
        }

        public record Order(string DeliveryRegion, decimal Cost);
    }
}
