using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.DTO;
using CurrencyApp.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace CurrencyApp.Infrastructure.Providers;

public sealed class ProviderCatalogService : IProviderCatalogService
{
    private readonly ApiOptions _opts;
    public ProviderCatalogService(IOptions<ApiOptions> opts) => _opts = opts.Value;

    public IReadOnlyList<ProviderDto> GetEnabled()
    {
        var def = _opts.Default?.ToLowerInvariant() ?? "nbp";
        var list = _opts.Items
            .Where(kv => kv.Value.Enabled)
            .Select(kv =>
            {
                var key = kv.Key.ToLowerInvariant();
                var name = kv.Value.DisplayName ?? kv.Key;
                var isDefault = key == def;
                return new ProviderDto(key, name, isDefault);
            })
            .OrderBy(p => p.Name)
            .ToList();

        return list;
    }
}
