using System.Net;
using System.Net.Http.Json;
using CurrencyApp.Infrastructure.Providers.Nbp.Models;
using Microsoft.Extensions.Options;

namespace CurrencyApp.Infrastructure.Providers.Nbp;

public sealed class NbpClient
{
    private readonly HttpClient _http;
    private readonly NbpOptions _opts;

    public NbpClient(HttpClient http, IOptions<NbpOptions> opts)
    {
        _http = http;
        _opts = opts.Value;
    }

    public async Task<NbpSeriesResponse?> GetSeriesAgainstPlnAsync(string code, DateOnly from, DateOnly to, CancellationToken ct)
    {
        var path = $"{_opts.TableAPath}/{code}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}{_opts.FormatQuery}";
        try
        {
            using var res = await _http.GetAsync(path, ct);

            if (res.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.NoContent)
                return null;

            res.EnsureSuccessStatusCode();

            return await res.Content.ReadFromJsonAsync<NbpSeriesResponse>(cancellationToken: ct);
        }
        catch (TaskCanceledException) when (!ct.IsCancellationRequested)
        {
            throw new HttpRequestException("NBP request timeout", null, HttpStatusCode.GatewayTimeout);
        }
        catch (HttpRequestException ex) when (ex.StatusCode is null)
        {
            throw new HttpRequestException("NBP network error", ex, HttpStatusCode.BadGateway);
        }
    }

    public async Task<IReadOnlyList<NbpTableRate>> GetCurrenciesFromTablesAsync(CancellationToken ct)
    {
        var unique = new Dictionary<string, NbpTableRate>(StringComparer.OrdinalIgnoreCase);

        foreach (var t in _opts.TablesForCurrencies)
        {
            var path = $"{_opts.TablesListPath}/{t}{_opts.FormatQuery}";
            try
            {
                using var res = await _http.GetAsync(path, ct);
                if (res.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.NoContent)
                    continue;

                res.EnsureSuccessStatusCode();

                var arr = await res.Content.ReadFromJsonAsync<NbpTableResponse[]>(cancellationToken: ct)
                          ?? Array.Empty<NbpTableResponse>();

                var table = arr.FirstOrDefault();
                if (table?.rates == null)
                    continue;

                foreach (var r in table.rates)
                    if (!string.IsNullOrWhiteSpace(r.code))
                        unique[r.code] = r;
            }
            catch
            {
                continue;
            }
        }

        return unique.Values.ToList();
    }
}
