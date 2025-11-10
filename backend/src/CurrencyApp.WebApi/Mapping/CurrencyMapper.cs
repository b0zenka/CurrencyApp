using CurrencyApp.WebApi.Contracts;

namespace CurrencyApp.WebApi.Mapping;

public static class CurrencyMapper
{
    public static IReadOnlyList<CurrencyItemDto> ToDto(IEnumerable<(string Code, string Name)> list) =>
        list.Select(x =>
        {
            var code = x.Code.ToUpperInvariant();
            return new CurrencyItemDto(code, x.Name, $"{code} - {x.Name}");
        }).ToList();
}