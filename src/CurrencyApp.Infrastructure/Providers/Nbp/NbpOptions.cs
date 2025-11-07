namespace CurrencyApp.Infrastructure.Providers.Nbp;
public sealed class NbpOptions
{
    public string TableAPath { get; set; } = "api/exchangerates/rates/A"; // kursy średnie
    public string FormatQuery { get; set; } = "?format=json";
}
