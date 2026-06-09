namespace ConsoleAppDotnetCore.PartialClassDemo
{
    internal partial class Employee
    {
        public void DoWork()
        {
            //System.Threading.Thread.Sleep(3000); // Simulate work
            Console.WriteLine($"{Name} is working.");
        }
    }
}
