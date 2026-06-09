namespace ConsoleAppDotnetCore.PartialClassDemo
{
    internal class PartialClassDemoRun
    {
        public static void Run()
        {
            Employee employee = new Employee { Name = "Ajay Kumar" };
            employee.DoWork();
            employee.GoToLunch();

            Console.WriteLine(employee.GetHashCode());

            Console.ReadLine();
        }
    }
}
