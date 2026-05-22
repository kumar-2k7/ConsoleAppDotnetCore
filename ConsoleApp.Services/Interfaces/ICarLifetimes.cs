using System;

namespace ConsoleApp.Services.Interfaces
{
    // Marker interfaces that extend ICar to demonstrate lifetimes
    public interface ICarTransient : ICar
    {
        Guid InstanceId { get; }
    }

    public interface ICarScoped : ICar
    {
        Guid InstanceId { get; }
    }
}
