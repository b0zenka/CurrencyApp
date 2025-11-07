namespace CurrencyApp.Application.Features.Exchange.GetRates;
public sealed record GetRatesQuery(string Api, string Source, string Target, DateOnly From, DateOnly To);
