using CurrencyApp.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApp.Infrastructure.Dispatching;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    public QueryDispatcher(IServiceProvider sp) => _serviceProvider = sp;

    public Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken ct)
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
        return handler.Handle(query, ct);
    }
}
