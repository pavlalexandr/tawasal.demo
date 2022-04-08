using Application.Services;
using Application.Services.Caching;
using Application.Services.Paging;
using AutoMapper;
using Domain.Entities.DummyApi;
using Domain.Repository;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit
{
    public class UsersServiceTest
    {

        private readonly IMapper mapperMock = Substitute.For<IMapper>();
        private readonly ICachingStrategyService cachingStrategyMock = Substitute.For<ICachingStrategyService>();
        private readonly IDummyApiRepository<User> dummyApiRepoMock = Substitute.For<IDummyApiRepository<User>>();
        private readonly IPagingService pagingServiceMock = Substitute.For<IPagingService>();
        private readonly ILogger<UsersService> loggerServiceMock = Substitute.For<ILogger<UsersService>>();

        [Fact]
        public async Task GetPaginatedView_IfExceptionThrown_LoggerCalled()
        {
            var service = new UsersService(mapperMock, cachingStrategyMock, dummyApiRepoMock, pagingServiceMock, loggerServiceMock);

            var result = await service.GetPaginatedView(1);

            loggerServiceMock.Received(1).LogError(Arg.Any<Exception>(), "GetPaginatedView");
        }
    }
}
