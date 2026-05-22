namespace ConsoleApp.Services.Interfaces
{
    public interface ICar
    {
        // Properties
        string Make { get; }
        string Model { get; }
        int Year { get; }
        bool IsRunning { get; }

        // Methods
        void StartEngine();
        void StopEngine();
        void Drive(double distance);
    }
}
