using CurrencyApp.Application.DTO;
using CurrencyApp.Domain.Currencies;

namespace CurrencyApp.Application.Features.Exchange.GetRates;
public static class Mapping
{
    public static RateSeriesDto ToDto(ExchangeRateSeries s, string dateFormat) =>
        new(
            s.Source.ToString(),
            s.Target.ToString(),
            s.Min(),
            s.Max(),
            s.Avg(),
            s.Points.Select(p =>
                new RatePointDto(p.Date.ToString(dateFormat), p.Rate)).ToList()
        );
}
