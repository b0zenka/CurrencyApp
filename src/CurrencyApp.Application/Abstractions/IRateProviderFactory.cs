namespace CurrencyApp.Application.Abstractions;
public interface IRateProviderFactory
{
    IExchangeRateProvider Get(string key);
}
