using Application.Services;
using Application.Services.Caching;
using Application.Services.Paging;
using AutoMapper;
using Domain.Entities.DummyApi;
using Domain.Repository;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit
{
    public class UserPostsServiceTest
    {
        private readonly IMapper mapperMock = Substitute.For<IMapper>();
        private readonly ICachingStrategyService cachingStrategyMock = Substitute.For<ICachingStrategyService>();
        private readonly IDummyApiRepository<UserPost> dummyApiRepoMock = Substitute.For<IDummyApiRepository<UserPost>>();
        private readonly IPagingService pagingServiceMock = Substitute.For<IPagingService>();
        private readonly ILogger<UserPostsService> loggerServiceMock = Substitute.For<ILogger<UserPostsService>>();

        [Fact]
        public async Task GetPaginatedView_IfExceptionThrown_LoggerCalled()
        {
            var service = new UserPostsService(mapperMock, cachingStrategyMock, dummyApiRepoMock, pagingServiceMock, loggerServiceMock);

            var result = await service.GetPaginatedView("1", 1);

            loggerServiceMock.Received(1).LogError(Arg.Any<Exception>(), "GetPaginatedView");
        }
    }
}
