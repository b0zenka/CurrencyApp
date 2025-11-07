using CurrencyApp.WebApi.Contracts;
using FluentValidation;

namespace CurrencyApp.WebApi.Validators;

public sealed class GetRatesRequestValidator : AbstractValidator<GetRatesRequest>
{
    public GetRatesRequestValidator()
    {
        RuleFor(x => x.Source)
            .NotEmpty()
            .Length(3)
            .WithMessage("'Source' must be an ISO currency code (3 characters).");

        RuleFor(x => x.Target)
            .NotEmpty()
            .Length(3)
            .WithMessage("'Target' must be an ISO currency code (3 characters).");


        RuleFor(x => x.To)
            .GreaterThanOrEqualTo(x => x.From)
            .WithMessage("'To' must be greater than or equal to 'From'.");

        RuleFor(x => x.From)
            .Must(BeTodayOrEarlier)
            .WithMessage("The 'From' date cannot be from the future.");

        RuleFor(x => x.To)
            .Must(BeTodayOrEarlier)
            .WithMessage("The 'To' date cannot be from the future.");
    }

    private static bool BeTodayOrEarlier(DateOnly? date)
        => !date.HasValue || date.Value <= DateOnly.FromDateTime(DateTime.UtcNow);
}
