namespace CurrencyApp.Application.DTO;
public sealed record RatePointDto(string Date, decimal Rate);
public sealed record RateSeriesDto(string Source, string Target, decimal? Min, decimal? Max, decimal? Avg, IReadOnlyList<RatePointDto> Points);
