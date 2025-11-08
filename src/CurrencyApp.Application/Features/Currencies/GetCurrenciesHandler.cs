using CurrencyApp.Application.Abstractions;
using CurrencyApp.Domain.Enums;

namespace CurrencyApp.Application.Features.Currencies.GetCurrencies;

public sealed class GetCurrenciesHandler : IQueryHandler<GetCurrenciesQuery, IReadOnlyList<(string Code, string Name)>>
{
    private readonly IRateProviderFactory _factory;
    private readonly IUiFormatting _uiFormatting;

    public GetCurrenciesHandler(IRateProviderFactory factory, IUiFormatting uiFormatting)
    {
        _factory = factory;
        _uiFormatting = uiFormatting;
    }

    public async Task<IReadOnlyList<(string Code, string Name)>> Handle(GetCurrenciesQuery query, CancellationToken ct)
    {
        var provider = _factory.Get(query.Api);
        var list = await provider.GetCurrenciesAsync(ct);

        if (list.Count == 0)
            return Array.Empty<(string, string)>();

        return _uiFormatting.CurrencySort switch
        {
            CurrencySort.CodeAsc => list.OrderBy(x => x.Code).ToList(),
            CurrencySort.CodeDesc => list.OrderByDescending(x => x.Code).ToList(),
            CurrencySort.NameAsc => list.OrderBy(x => x.Name).ToList(),
            CurrencySort.NameDesc => list.OrderByDescending(x => x.Name).ToList(),
            _ => list
        };
    }
}