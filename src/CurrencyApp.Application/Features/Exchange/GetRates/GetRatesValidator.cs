using FluentValidation;
namespace CurrencyApp.Application.Features.Exchange.GetRates;
public sealed class GetRatesValidator : AbstractValidator<GetRatesQuery>
{
    public GetRatesValidator()
    {
        RuleFor(x => x.Source).NotEmpty().Length(3);
        RuleFor(x => x.Target).NotEmpty().Length(3);
        RuleFor(x => x.To).GreaterThanOrEqualTo(x => x.From);
    }
}
