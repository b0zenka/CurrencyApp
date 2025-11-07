using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.DTO;

namespace CurrencyApp.Application.Features.Exchange.GetRates;

public sealed class GetRatesHandler
{
    private readonly IRateProviderFactory _factory;
    public GetRatesHandler(IRateProviderFactory factory) => _factory = factory;

    public async Task<RateSeriesDto?> Handle(GetRatesQuery q, CancellationToken ct)
    {
        var provider = _factory.Get(q.Api);

        var series = await provider.GetSeriesAsync(q.Source, q.Target, q.From, q.To, ct);
        if (series.IsEmpty) 
            return null;

        return Mapping.ToDto(series);
    }
}
