using CurrencyApp.Application.DTO;

namespace CurrencyApp.Application.Abstractions;

public interface IProviderCatalogService
{
    IReadOnlyList<ProviderDto> GetEnabled();
}
