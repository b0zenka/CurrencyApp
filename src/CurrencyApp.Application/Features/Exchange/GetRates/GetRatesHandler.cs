using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.DTO;

namespace CurrencyApp.Application.Features.Exchange.GetRates;
public sealed class GetRatesHandler
{
    private readonly IExchangeRateProvider _provider;
    public GetRatesHandler(IExchangeRateProvider provider) => _provider = provider;

    public async Task<RateSeriesDto?> Handle(GetRatesQuery q, CancellationToken ct)
    {
        var series = await _provider.GetSeriesAsync(q.Source, q.Target, q.From, q.To, ct);
        if (series.IsEmpty) 
            return null;

        return Mapping.ToDto(series);
    }
}
