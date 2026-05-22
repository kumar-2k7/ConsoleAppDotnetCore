using ConsoleApp.Services.Interfaces;

namespace ConsoleApp.Services.Services.Cars
{
    public class Sedan : ICar
    {
        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public bool IsRunning { get; private set; }

        public Sedan(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public void StartEngine()
        {
            IsRunning = true;
            Console.WriteLine($"{Make} {Model} engine started. Vroom!");
        }

        public void StopEngine()
        {
            IsRunning = false;
            Console.WriteLine("Engine stopped.");
        }

        public void Drive(double distance)
        {
            if (IsRunning)
                Console.WriteLine($"Driving the sedan for {distance} miles.");
            else
                Console.WriteLine("Check engine light! (You forgot to start it).");
        }
    }
}
