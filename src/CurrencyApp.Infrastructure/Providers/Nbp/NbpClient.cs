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
}
