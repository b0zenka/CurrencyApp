using CurrencyApp.Domain.Currencies;

namespace CurrencyApp.Application.Abstractions;

public interface IExchangeRateProvider
{
    Task<IReadOnlyList<(string Code, string Name)>> GetCurrenciesAsync(CancellationToken ct);
    Task<ExchangeRateSeries> GetSeriesAsync(string sourceCode, string targetCode, DateOnly from, DateOnly to, CancellationToken ct);
}
