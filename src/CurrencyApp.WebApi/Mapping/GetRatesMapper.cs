using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.Features.Exchange.GetRates;
using CurrencyApp.WebApi.Contracts;

namespace CurrencyApp.WebApi.Mapping;

public static class GetRatesMapper
{
    public static GetRatesQuery ToQuery(GetRatesRequest req, IDateTimeProvider clock)
    {
        var today = DateOnly.FromDateTime(clock.UtcNow);

        return new GetRatesQuery(
            Api: string.IsNullOrWhiteSpace(req.Api) ? "nbp" : req.Api.ToLowerInvariant(),
            Source: req.Source.ToUpperInvariant(),
            Target: req.Target.ToUpperInvariant(),
            From: req.From ?? today,
            To: req.To ?? today
        );
    }
}
