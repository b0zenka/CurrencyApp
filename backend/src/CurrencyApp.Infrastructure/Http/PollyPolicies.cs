using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace CurrencyApp.Infrastructure.Http;
public static class PollyPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> Retry() =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(200 * attempt));
}
