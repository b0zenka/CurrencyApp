using CurrencyApp.Domain.Enums;

namespace CurrencyApp.Application.Abstractions;

public interface IUiFormatting
{
    CurrencySort CurrencySort { get; }
}