namespace CurrencyApp.Application.Abstractions;

public interface IQueryDispatcher
{
    Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken ct);
}
