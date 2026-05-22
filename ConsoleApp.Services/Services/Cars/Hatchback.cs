using ConsoleApp.Services.Interfaces;

namespace ConsoleApp.Services.Services.Cars
{
    public class Hatchback : ICar
    {
        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public bool IsRunning { get; private set; }

        // Unique property for Hatchbacks
        public double CargoVolumeLitres { get; init; }

        public Hatchback(string make, string model, int year, double cargoVolume)
        {
            Make = make;
            Model = model;
            Year = year;
            CargoVolumeLitres = cargoVolume;
        }

        public void StartEngine()
        {
            IsRunning = true;
            Console.WriteLine($"{Make} {Model} (Hatchback) hums to life quietly.");
        }

        public void StopEngine() => IsRunning = false;

        public void Drive(double distance)
        {
            if (IsRunning) Console.WriteLine($"Zipping through traffic for {distance}km.");
        }
    }
}
