using System;
using ConsoleApp.Services.Interfaces;

namespace ConsoleApp.Services.Services.Cars
{
    public class ScopedCar : ICarScoped
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public bool IsRunning { get; private set; }

        public ScopedCar()
        {
            Make = "ScopedMake";
            Model = "ScopedModel";
            Year = DateTime.Now.Year;
        }

        public void StartEngine()
        {
            IsRunning = true;
            Console.WriteLine($"[Scoped {InstanceId}] {Make} {Model} started.");
        }

        public void StopEngine()
        {
            IsRunning = false;
            Console.WriteLine($"[Scoped {InstanceId}] stopped.");
        }

        public void Drive(double distance)
        {
            if (IsRunning) Console.WriteLine($"[Scoped {InstanceId}] Driving {distance}km");
            else Console.WriteLine($"[Scoped {InstanceId}] Can't drive; engine off.");
        }
    }
}
