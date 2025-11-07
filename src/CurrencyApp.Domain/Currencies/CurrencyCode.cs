using CurrencyApp.Domain.Common;
namespace CurrencyApp.Domain.Currencies;

public sealed class CurrencyCode : ValueObject
{
    public string Code { get; }
    private CurrencyCode(string code) => Code = code;
    public static CurrencyCode Of(string? code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length is < 3 or > 3) throw new ArgumentException("Invalid currency code");
        return new CurrencyCode(code.ToUpperInvariant());
    }
    protected override IEnumerable<object?> GetEqualityComponents() { yield return Code; }
    public override string ToString() => Code;
}
