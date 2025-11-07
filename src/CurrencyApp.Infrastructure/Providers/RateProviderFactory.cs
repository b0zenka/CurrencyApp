using CurrencyApp.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApp.Infrastructure.Providers;

public sealed class RateProviderFactory : IRateProviderFactory
{
    private readonly IServiceProvider _serviceProvider;
    public RateProviderFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public IExchangeRateProvider Get(string key)
        => _serviceProvider.GetRequiredKeyedService<IExchangeRateProvider>(key.ToLowerInvariant());
}
