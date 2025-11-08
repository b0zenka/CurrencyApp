using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.DTO;
using CurrencyApp.Application.Features.Currencies.GetCurrencies;
using CurrencyApp.Application.Features.Exchange.GetRates;
using CurrencyApp.Infrastructure.Configuration;
using CurrencyApp.WebApi.Contracts;
using CurrencyApp.WebApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CurrencyApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExchangeController : ControllerBase
{
    private readonly IOptions<ApiOptions> _apiOptions;
    private readonly IProviderCatalogService _catalog;
    private readonly IDateTimeProvider _clock;
    private readonly IQueryDispatcher _dispatcher;

    public ExchangeController( 
        IOptions<ApiOptions> apiOptions,
        IProviderCatalogService catalog,
        IDateTimeProvider clock,
        IQueryDispatcher dispatcher)
    {
        _apiOptions = apiOptions;
        _catalog = catalog;
        _clock = clock;
        _dispatcher = dispatcher;
    }

    [HttpPost("rates")]
    [ProducesResponseType(typeof(RateSeriesDto), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    public async Task<IActionResult> GetRates(
        [FromBody] GetRatesRequest req,
        CancellationToken ct)
    {
        var query = GetRatesMapper.ToQuery(req, _clock);

        var result = await _dispatcher.Query<GetRatesQuery, RateSeriesDto?>(query, ct);
        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("providers")]
    [ProducesResponseType(typeof(IEnumerable<ProviderDto>), 200)]
    [ProducesResponseType(204)]
    public IActionResult GetProviders()
    {
        var list = _catalog.GetEnabled();
        return list.Count == 0 ? NoContent() : Ok(list);
    }

    [HttpGet("currencies")]
    [ProducesResponseType(typeof(IEnumerable<CurrencyItemDto>), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GetCurrencies([FromQuery] string? api, CancellationToken ct)
    {
        var key = string.IsNullOrWhiteSpace(api)
            ? (_apiOptions.Value.Default?.ToLowerInvariant() ?? "nbp")
            : api.ToLowerInvariant();

        var result = await _dispatcher.Query<GetCurrenciesQuery, IReadOnlyList<(string, string)>>(
            new GetCurrenciesQuery(key), ct);
        
        if (result.Count == 0)
            return NoContent();

        return Ok(CurrencyMapper.ToDto(result));
    }
}

