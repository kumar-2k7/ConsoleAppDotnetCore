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
    }
}
