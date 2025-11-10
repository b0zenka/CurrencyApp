namespace CurrencyApp.WebApi.Contracts;
public sealed record GetRatesRequest(string Api, string Source, string Target, DateOnly? From, DateOnly? To);
