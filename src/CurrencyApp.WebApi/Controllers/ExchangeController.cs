using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.DTO;
using CurrencyApp.Application.Features.Exchange.GetRates;
using CurrencyApp.WebApi.Contracts;
using CurrencyApp.WebApi.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExchangeController : ControllerBase
{
    private readonly GetRatesHandler _handler;
    private readonly IDateTimeProvider _clock;

    public ExchangeController(GetRatesHandler handler, IDateTimeProvider clock)
    {
        _handler = handler; _clock = clock;
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
}

