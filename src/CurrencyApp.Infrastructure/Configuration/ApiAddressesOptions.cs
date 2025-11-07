namespace CurrencyApp.Infrastructure.Configuration;
public sealed class ApiAddressesOptions
{
    public const string SectionName = "Apis";
    public NbpOptions Nbp { get; set; } = new();
    public sealed class NbpOptions { public string BaseUrl { get; set; } = "https://api.nbp.pl/"; }
}
