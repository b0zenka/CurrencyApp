using CurrencyApp.Application.Abstractions;
using CurrencyApp.Infrastructure.Configuration;
using CurrencyApp.Infrastructure.Dispatching;
using CurrencyApp.Infrastructure.Http;
using CurrencyApp.Infrastructure.Providers;
using CurrencyApp.Infrastructure.Providers.Nbp;
using CurrencyApp.Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CurrencyApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        // Options
        services.Configure<ApiOptions>(cfg.GetSection(ApiOptions.SectionName));
        services.Configure<UiFormattingOptions>(cfg.GetSection(UiFormattingOptions.SectionName));
        services.Configure<NbpOptions>(cfg.GetSection("Nbp"));

        // Adapters
        services.AddSingleton<IUiFormatting, UiFormattingAdapter>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        // Query dispatcher
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        // Providers
        services.AddSingleton<IProviderCatalogService, ProviderCatalogService>();
        services.AddTransient<IRateProviderFactory, RateProviderFactory>();

        // HTTP clients
        services.AddHttpClient<NbpClient>((sp, http) =>
        {
            var nbp = sp.GetRequiredService<IOptions<NbpOptions>>().Value;
            http.BaseAddress = new Uri(nbp.BaseUrl.TrimEnd('/') + "/");
        }).AddPolicyHandler(PollyPolicies.Retry());

        // Keyed providers
        services.AddKeyedTransient<IExchangeRateProvider, NbpExchangeRateProvider>("nbp");

        return services;
    }
}
