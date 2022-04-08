using Application.Services.DateTimeService;
using Blazored.LocalStorage;
using Infrastructure.Services.Caching;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Threading;
using Domain.Models.Caching;
using System;

namespace Tests.Unit.Caching
{
    public class TemporaryClientCachingServiceTest
    {
        private readonly ILocalStorageService _localStorageServiceMock = Substitute.For<ILocalStorageService>();
        private readonly IDateTimeService _dateTimeServiceMock = Substitute.For<IDateTimeService>();

        [Fact]
        public async Task GetAsync_IfKeyNotExists_ReturnsDefault()
        {
            var tempCacheKey = "someKey";
            _localStorageServiceMock.ContainKeyAsync(tempCacheKey, CancellationToken.None).Returns(false);

            var service = new TemporaryClientCachingService(_localStorageServiceMock, new Domain.Models.Settings.ApplicationSettingsModel()
            {
                LocalStorageCacheTimeoutInMinutes = 5
            }, _dateTimeServiceMock);

            var result = await service.GetAsync<TemporaryClientCachingServiceTest>(tempCacheKey, CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_IfKeyExists_ShouldCallGetItemAsync()
        {
            var tempCacheKey = "someKey";
            _localStorageServiceMock.ContainKeyAsync(tempCacheKey, Arg.Any<CancellationToken>()).Returns(true);

            var service = new TemporaryClientCachingService(_localStorageServiceMock, new Domain.Models.Settings.ApplicationSettingsModel()
            {
                LocalStorageCacheTimeoutInMinutes = 5
            }, _dateTimeServiceMock);

            var result = await service.GetAsync<TemporaryClientCachingServiceTest>(tempCacheKey, CancellationToken.None);

            await _localStorageServiceMock.Received(1).GetItemAsync<CachedValue<TemporaryClientCachingServiceTest>>(tempCacheKey, CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_IfCachedItemIsTimedOut_ReturnsDefault()
        {
            var tempCacheKey = "someKey";

            _localStorageServiceMock.ContainKeyAsync(tempCacheKey, Arg.Any<CancellationToken>()).Returns(true);

            _localStorageServiceMock.GetItemAsync<CachedValue<object>>(tempCacheKey, CancellationToken.None).Returns(ValueTask.FromResult(new CachedValue<object>()
            {
                Time = TimeSpan.FromMinutes(1),
                Value = 1
            }));

            _dateTimeServiceMock.GetNow().Returns(new DateTime().AddMinutes(5));

            var service = new TemporaryClientCachingService(_localStorageServiceMock, new Domain.Models.Settings.ApplicationSettingsModel()
            {
                LocalStorageCacheTimeoutInMinutes = 1
            }, _dateTimeServiceMock);

            var result = await service.GetAsync<object>(tempCacheKey, CancellationToken.None);

            await _localStorageServiceMock.Received(1).GetItemAsync<CachedValue<object>>(tempCacheKey, CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_IfCachedItemIsActive_ReturnsCachedItemValue()
        {
            //
            var tempCacheKey = "someKey";

            _localStorageServiceMock.ContainKeyAsync(tempCacheKey, Arg.Any<CancellationToken>()).Returns(true);

            _localStorageServiceMock.GetItemAsync<CachedValue<object>>(tempCacheKey, CancellationToken.None).Returns(ValueTask.FromResult(new CachedValue<object>()
            {
                Time = TimeSpan.FromMinutes(5),
                Value = 1
            }));

            _dateTimeServiceMock.GetNow().Returns(new DateTime().AddMinutes(5));

            var service = new TemporaryClientCachingService(_localStorageServiceMock, new Domain.Models.Settings.ApplicationSettingsModel()
            {
                LocalStorageCacheTimeoutInMinutes = 5
            }, _dateTimeServiceMock);
            //
            var result = await service.GetAsync<object>(tempCacheKey, CancellationToken.None);
            //
            await _localStorageServiceMock.Received(1).GetItemAsync<CachedValue<object>>(tempCacheKey, CancellationToken.None);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task SetAsync_CallSetItemAsync_WithProperlyParameters()
        {
            //
            var tempCacheKey = "someKey";
            var dataToCache = 1;
            var timeStamp = new DateTime().AddMinutes(5);
            _dateTimeServiceMock.GetNow().Returns(timeStamp);

            var service = new TemporaryClientCachingService(_localStorageServiceMock, new Domain.Models.Settings.ApplicationSettingsModel()
            {
                LocalStorageCacheTimeoutInMinutes = 5
            }, _dateTimeServiceMock);
            //
            await service.SetAsync(tempCacheKey, dataToCache, CancellationToken.None);
            //
            await _localStorageServiceMock.Received(1).SetItemAsync(tempCacheKey, Arg.Is<CachedValue<int>>(p => p.Time == timeStamp.TimeOfDay && p.Value == dataToCache), CancellationToken.None);
        }
    }
}
