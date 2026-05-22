using ConsoleApp.Services.Interfaces;

namespace ConsoleApp.Services.Services.Cars
{
    public class Suv : ICar
    {
        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public bool IsRunning { get; private set; }

        // Unique property for SUVs
        public bool HasAwd { get; init; }

        public Suv(string make, string model, int year, bool hasAwd)
        {
            Make = make;
            Model = model;
            Year = year;
            HasAwd = hasAwd;
        }

        public void StartEngine()
        {
            IsRunning = true;
            Console.WriteLine($"{Make} {Model} (SUV) engine roars to life!");
        }

        public void StopEngine() => IsRunning = false;

        public void Drive(double distance)
        {
            string mode = HasAwd ? "all-terrain mode" : "road mode";
            if (IsRunning) Console.WriteLine($"Driving {distance}km in {mode}.");
        }
    }
}
