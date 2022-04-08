namespace Application.Services.Caching
{
    public class CachingStrategyService : ICachingStrategyService
    {
        private readonly ITemporaryClientCachingService _clientCachingService;

        public CachingStrategyService(ITemporaryClientCachingService clientCachingService)
        {
            _clientCachingService = clientCachingService;
        }

        public async Task<T> ApplyAsync<T>(string cacheKey, Func<CancellationToken, Task<T>> updateResultsFunc, CancellationToken cancellationToken = default)
        {
            var cachedData = await _clientCachingService.GetAsync<T>(cacheKey, cancellationToken);

            if (cachedData == null)
            {
                cachedData = await updateResultsFunc(cancellationToken);
                await _clientCachingService.SetAsync(cacheKey, cachedData, cancellationToken);
            }

            return cachedData;
        }
    }
}
