using CurrencyApp.Application.Abstractions;
using CurrencyApp.Domain.Enums;
using Microsoft.Extensions.Options;

namespace CurrencyApp.Infrastructure.Configuration;

public sealed class UiFormattingAdapter : IUiFormatting
{
    private readonly UiFormattingOptions _opts;
    public UiFormattingAdapter(IOptions<UiFormattingOptions> opts) => _opts = opts.Value;
    
    public CurrencySort CurrencySort => _opts.CurrencySort;
    public string DateFormat => _opts.DateFormat;
}