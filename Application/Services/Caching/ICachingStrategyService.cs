
namespace Application.Services.Caching
{
    public interface ICachingStrategyService
    {
        Task<T> ApplyAsync<T>(string cacheKey, Func<CancellationToken, Task<T>> updateResultsFunc, CancellationToken cancellationToken = default);
    }
}