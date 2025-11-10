namespace CurrencyApp.Infrastructure.Providers.Nbp;

public sealed class NbpRateItem { public string no { get; set; } = ""; public string effectiveDate { get; set; } = ""; public decimal mid { get; set; } }
public sealed class NbpSeriesResponse { public string table { get; set; } = ""; public string currency { get; set; } = ""; public string code { get; set; } = ""; public List<NbpRateItem> rates { get; set; } = new(); }
