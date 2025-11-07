namespace CurrencyApp.Domain.Currencies;

public sealed record ExchangeRatePoint(DateOnly Date, decimal Rate);
public sealed class ExchangeRateSeries
{
    public CurrencyCode Source { get; }
    public CurrencyCode Target { get; }
    public IReadOnlyList<ExchangeRatePoint> Points { get; }
    public ExchangeRateSeries(CurrencyCode src, CurrencyCode trg, IReadOnlyList<ExchangeRatePoint> points)
    {
        Source = src; Target = trg; Points = points;
    }
    public bool IsEmpty => Points.Count == 0;
    public decimal? Min() => Points.Count == 0 ? null : Points.Min(p => p.Rate);
    public decimal? Max() => Points.Count == 0 ? null : Points.Max(p => p.Rate);
    public decimal? Avg() => Points.Count == 0 ? null : Points.Average(p => p.Rate);
}
