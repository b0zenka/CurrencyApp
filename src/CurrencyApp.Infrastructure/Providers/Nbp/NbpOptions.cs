namespace CurrencyApp.Infrastructure.Providers.Nbp;
public sealed class NbpOptions
{
    public string BaseUrl { get; set; } = "https://api.nbp.pl/";
    public string TableAPath { get; set; } = "api/exchangerates/rates/A";
    public string FormatQuery { get; set; } = "?format=json";
}
