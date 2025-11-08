namespace CurrencyApp.Infrastructure.Providers.Nbp.Models;

public sealed class NbpTableResponse
{
    public string table { get; set; } = "";
    public string no { get; set; } = "";
    public string effectiveDate { get; set; } = "";
    public List<NbpTableRate> rates { get; set; } = new();
}
