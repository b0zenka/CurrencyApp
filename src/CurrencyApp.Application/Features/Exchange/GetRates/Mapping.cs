using CurrencyApp.Application.DTO;
using CurrencyApp.Domain.Currencies;

namespace CurrencyApp.Application.Features.Exchange.GetRates;
public static class Mapping
{
    public static RateSeriesDto ToDto(ExchangeRateSeries s) =>
        new(s.Source.ToString(), s.Target.ToString(), s.Min(), s.Max(), s.Avg(),
            s.Points.Select(p => new RatePointDto(p.Date, p.Rate)).ToList());
}
