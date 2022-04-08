namespace Application.Services.Caching
{
    public interface ITemporaryClientCachingService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
        Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);
    }
}
