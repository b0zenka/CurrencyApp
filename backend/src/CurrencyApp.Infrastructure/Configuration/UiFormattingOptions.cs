using CurrencyApp.Domain.Enums;
namespace CurrencyApp.Infrastructure.Configuration;
public sealed class UiFormattingOptions
{
    public const string SectionName = "Ui";
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public CurrencySort CurrencySort { get; set; } = CurrencySort.CodeAsc;
}
