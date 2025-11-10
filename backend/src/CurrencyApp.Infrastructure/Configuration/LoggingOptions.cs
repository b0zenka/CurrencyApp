namespace CurrencyApp.Infrastructure.Configuration;
public sealed class LoggingOptions
{
    public const string SectionName = "LoggingOptions";
    public string Path { get; set; } = "logs/log-.txt";
    public string MinimumLevel { get; set; } = "Information";
}
