using CurrencyApp.Application.Abstractions;
using CurrencyApp.Domain.Currencies;

namespace CurrencyApp.Infrastructure.Providers.Nbp;
public sealed class NbpExchangeRateProvider : IExchangeRateProvider
{
    private readonly NbpClient _client;
    public NbpExchangeRateProvider(NbpClient client) => _client = client;

    public async Task<ExchangeRateSeries> GetSeriesAsync(string sourceCode, string targetCode, DateOnly from, DateOnly to, CancellationToken ct)
    {
        var src = await _client.GetSeriesAgainstPlnAsync(sourceCode, from, to, ct) ?? new NbpSeriesResponse();
        var tgt = targetCode.Equals("PLN", StringComparison.OrdinalIgnoreCase)
            ? new NbpSeriesResponse { code = "PLN", currency = "złoty", rates = src.rates.Select(r => new NbpRateItem { effectiveDate = r.effectiveDate, mid = 1m }).ToList() }
            : await _client.GetSeriesAgainstPlnAsync(targetCode, from, to, ct) ?? new NbpSeriesResponse();

        var mapTgt = tgt.rates.ToDictionary(r => DateOnly.Parse(r.effectiveDate));
        var points = new List<ExchangeRatePoint>();

        foreach (var s in src.rates)
        {
            var d = DateOnly.Parse(s.effectiveDate);
            if (mapTgt.TryGetValue(d, out var t) && t.mid != 0)
            {
                var rate = sourceCode.Equals("PLN", StringComparison.OrdinalIgnoreCase)
                    ? 1m / t.mid
                    : s.mid / t.mid;

                points.Add(new ExchangeRatePoint(d, decimal.Round(rate, 6)));
            }
        }

        return new ExchangeRateSeries(CurrencyCode.Of(sourceCode), CurrencyCode.Of(targetCode), points.OrderBy(p => p.Date).ToList());
    }

    public async Task<IReadOnlyList<(string Code, string Name)>> GetCurrenciesAsync(CancellationToken ct)
    {
        var list = new List<(string Code, string Name)>
        {
            ("PLN", "złoty polski")
        };

        var rates = await _client.GetCurrenciesFromTablesAsync(ct);

        foreach (var r in rates)
        {
            var code = r.code?.ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(code)) 
                continue;

            if (code == "PLN") 
                continue;

            var name = string.IsNullOrWhiteSpace(r.currency) ? code : r.currency;
            list.Add((code, name));
        }

        return list;
    }

}
