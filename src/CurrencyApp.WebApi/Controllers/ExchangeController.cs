using CurrencyApp.Application.DTO;
using CurrencyApp.Application.Features.Exchange.GetRates;
using CurrencyApp.WebApi.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExchangeController : ControllerBase
{
    private readonly GetRatesHandler _handler;

    public ExchangeController(GetRatesHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("rates")]
    [ProducesResponseType(typeof(RateSeriesDto), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    public async Task<IActionResult> GetRates([FromBody] GetRatesRequest req, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var query = new GetRatesQuery(
            Api: string.IsNullOrWhiteSpace(req.Api) ? "nbp" : req.Api.ToLowerInvariant(),
            Source: req.Source.ToUpperInvariant(),
            Target: req.Target.ToUpperInvariant(),
            From: req.From ?? today,
            To: req.To ?? today
        );

        var result = await _handler.Handle(query, ct);
        return result is null ? NoContent() : Ok(result);
    }
}

