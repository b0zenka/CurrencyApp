using CurrencyApp.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApp.Infrastructure.Providers;

public sealed class ProviderFactory
{
    private readonly IServiceProvider _serviceProvider;
    public ProviderFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public IExchangeRateProvider Get(string key)
    {
        key = key.ToLowerInvariant();
        return _serviceProvider.GetRequiredKeyedService<IExchangeRateProvider>(key);
    }
}
