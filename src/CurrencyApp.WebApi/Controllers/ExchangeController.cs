using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.DTO;
using CurrencyApp.Application.Features.Currencies.GetCurrencies;
using CurrencyApp.Application.Features.Exchange.GetRates;
using CurrencyApp.Domain.Enums;
using CurrencyApp.Infrastructure.Configuration;
using CurrencyApp.WebApi.Contracts;
using CurrencyApp.WebApi.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CurrencyApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExchangeController : ControllerBase
{
    private readonly GetRatesHandler _handler;
    private readonly IDateTimeProvider _clock;
    private readonly GetCurrenciesHandler _currenciesHandler;
    private readonly IOptions<ApiOptions> _apiOptions;

    public ExchangeController(
        GetRatesHandler handler,
        GetCurrenciesHandler currenciesHandler, 
        IOptions<ApiOptions> apiOptions,
        IDateTimeProvider clock)
    {
        _handler = handler; 
        _currenciesHandler = currenciesHandler;
        _apiOptions = apiOptions;
        _clock = clock;
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

        var result = await _handler.Handle(query, ct);
        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("providers")]
    [ProducesResponseType(typeof(IEnumerable<ProviderDto>), 200)]
    [ProducesResponseType(204)]
    public IActionResult GetProviders([FromServices] IProviderCatalogService catalog)
    {
        var list = catalog.GetEnabled();
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

        var result = await _currenciesHandler.Handle(new GetCurrenciesQuery(key), ct);
        if (result.Count == 0)
            return NoContent();

        return Ok(CurrencyMapper.ToDto(result));
    }
}

