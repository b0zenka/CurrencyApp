using CurrencyApp.Infrastructure.Providers.Nbp.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CurrencyApp.Infrastructure.Providers.Nbp;
public sealed class NbpClient
{
    private readonly HttpClient _http;
    private readonly NbpOptions _opts;
    public NbpClient(HttpClient http, IOptions<NbpOptions> opts) { _http = http; _opts = opts.Value; }

    public async Task<NbpSeriesResponse?> GetSeriesAgainstPlnAsync(string code, DateOnly from, DateOnly to, CancellationToken ct)
    {
        var path = $"{_opts.TableAPath}/{code}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}{_opts.FormatQuery}";
        return await _http.GetFromJsonAsync<NbpSeriesResponse>(path, cancellationToken: ct);
    }

    public async Task<IReadOnlyList<NbpTableRate>> GetCurrenciesFromTablesAsync(CancellationToken ct)
    {
        var unique = new Dictionary<string, NbpTableRate>(StringComparer.OrdinalIgnoreCase);

        foreach (var t in _opts.TablesForCurrencies)
        {
            var path = $"{_opts.TablesListPath}/{t}{_opts.FormatQuery}";

            var arr = await _http.GetFromJsonAsync<NbpTableResponse[]>(path, cancellationToken: ct) ?? Array.Empty<NbpTableResponse>();
            var table = arr.FirstOrDefault();

            if (table == null || table.rates == null) 
                continue;

            foreach (var r in table.rates)
            {
                if (string.IsNullOrWhiteSpace(r.code)) 
                    continue;
                unique[r.code] = r;
            }
        }

        return unique.Values.ToList();
    }
}
