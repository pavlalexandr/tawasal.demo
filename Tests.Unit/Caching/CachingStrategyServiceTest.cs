using Application.Services.Caching;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using FluentAssertions;

namespace Tests.Unit.Caching
{
    public class CachingStrategyServiceTest
    {
        private readonly ITemporaryClientCachingService _cacheServiceMock = Substitute.For<ITemporaryClientCachingService>();

        [Fact]
        public async Task ApplyAsync_IfCachedDataEmpty_GetDataAndSave()
        {
            var key = "someKey";

            var valueToCache = 1;

            _cacheServiceMock.GetAsync<object>(key).Returns(Task.FromResult((object)null));

            var service = new CachingStrategyService(_cacheServiceMock);

            var result = await service.ApplyAsync(key, (ct) => Task.FromResult((object)valueToCache));

            await _cacheServiceMock.Received(1).SetAsync(key, valueToCache);

            result.Should().Be(valueToCache);
        }

        [Fact]
        public async Task ApplyAsync_IfCachedDataExists_DoNotSave()
        {
            var key = "someKey";

            var valueToCache = 1;

            _cacheServiceMock.GetAsync<object>(key).Returns(Task.FromResult((object)valueToCache));

            var service = new CachingStrategyService(_cacheServiceMock);

            var result = await service.ApplyAsync(key, (ct) => Task.FromResult((object)null));

            await _cacheServiceMock.Received(0).SetAsync(key, valueToCache);

            result.Should().Be(valueToCache);
        }
    }
}
