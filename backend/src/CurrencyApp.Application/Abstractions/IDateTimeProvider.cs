namespace CurrencyApp.Application.Abstractions;

public interface IDateTimeProvider 
{
    DateTime UtcNow { get; } 
}
