using Application.Model;
using Application.Services.Caching;
using Application.Services.Paging;
using AutoMapper;
using Domain.Entities.DummyApi;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserPostsService
    {
        private readonly IMapper _mapper;
        private readonly ICachingStrategyService _cachingStrategyService;
        private readonly IDummyApiRepository<UserPost> _dummyUserPostRepository;
        private readonly IPagingService _pagingService;
        private readonly ILogger<UserPostsService> _logger;

        public UserPostsService(IMapper mapper,
            ICachingStrategyService cachingStrategyService,
            IDummyApiRepository<UserPost> dummyUserPostRepository,
            IPagingService pagingService,
            ILogger<UserPostsService> logger)
        {
            _mapper = mapper;
            _cachingStrategyService = cachingStrategyService;
            _dummyUserPostRepository = dummyUserPostRepository;
            _pagingService = pagingService;
            _logger = logger;
        }

        public async Task<PaginatedView<UserPostView>?> GetPaginatedView(string userId, int page, int itemsPerPage = 10, CancellationToken cancellationToken = default)
        {
            try
            {
                var data = await _cachingStrategyService.ApplyAsync($"userPostList_{page}_{itemsPerPage}_{userId}",
                (ct) => _dummyUserPostRepository.GetListByParent(userId, page, itemsPerPage, ct),
                cancellationToken);

                var result = _mapper.Map<PaginatedView<UserPostView>>(data);

                result.TotalPages = _pagingService.GetTotalPages(data.Total, data.Limit);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPaginatedView");
                return null;
            }
        }
    }
}
