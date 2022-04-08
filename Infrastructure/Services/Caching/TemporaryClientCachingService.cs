using Application.Services.Caching;
using Application.Services.DateTimeService;
using Blazored.LocalStorage;
using Domain.Models.Caching;
using Domain.Models.Settings;

namespace Infrastructure.Services.Caching
{
    public class TemporaryClientCachingService : ITemporaryClientCachingService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IDateTimeService _dateTimeService;
        private readonly int _timeoutInMinutes;

        public TemporaryClientCachingService(ILocalStorageService localStorageService, ApplicationSettingsModel applicationSettingsModel, IDateTimeService dateTimeService)
        {
            _localStorageService = localStorageService;
            _dateTimeService = dateTimeService;
            _timeoutInMinutes = applicationSettingsModel.LocalStorageCacheTimeoutInMinutes;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            if (await _localStorageService.ContainKeyAsync(key, cancellationToken))
            {
                var cachedValue = await _localStorageService.GetItemAsync<CachedValue<T>>(key, cancellationToken);

                if (cachedValue != null && cachedValue.Time.Add(TimeSpan.FromMinutes(_timeoutInMinutes)) > _dateTimeService.GetNow().TimeOfDay)
                    return cachedValue.Value;
            }
            return default(T);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        {
            await _localStorageService.SetItemAsync(key, new CachedValue<T>()
            {
                Value = value,
                Time = _dateTimeService.GetNow().TimeOfDay
            }, cancellationToken);
        }
    }
}
