using CurrencyApp.Application.Abstractions;

namespace CurrencyApp.Infrastructure.Time;

public sealed class SystemDateTimeProvider : IDateTimeProvider 
{ 
    public DateTime UtcNow => DateTime.UtcNow; 
}

