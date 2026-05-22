using System;
using ConsoleApp.Services.Interfaces;

namespace ConsoleApp.Services.Services.Cars
{
    public class TransientCar : ICarTransient
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public bool IsRunning { get; private set; }

        public TransientCar()
        {
            Make = "TransientMake";
            Model = "TransientModel";
            Year = DateTime.Now.Year;
        }

        public void StartEngine()
        {
            IsRunning = true;
            Console.WriteLine($"[Transient {InstanceId}] {Make} {Model} started.");
        }

        public void StopEngine()
        {
            IsRunning = false;
            Console.WriteLine($"[Transient {InstanceId}] stopped.");
        }

        public void Drive(double distance)
        {
            if (IsRunning) Console.WriteLine($"[Transient {InstanceId}] Driving {distance}km");
            else Console.WriteLine($"[Transient {InstanceId}] Can't drive; engine off.");
        }
    }
}
