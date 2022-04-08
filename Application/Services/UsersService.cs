using Application.Model;
using Application.Services.Caching;
using Application.Services.Paging;
using AutoMapper;
using Domain.Entities.DummyApi;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UsersService
    {
        private readonly IMapper _mapper;
        private readonly ICachingStrategyService _cachingStrategyService;
        private readonly IDummyApiRepository<User> _dummyUserRepository;
        private readonly IPagingService _pagingService;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IMapper mapper,
            ICachingStrategyService cachingStrategyService,
            IDummyApiRepository<User> dummyUserRepository,
            IPagingService pagingService,
            ILogger<UsersService> logger)
        {
            _mapper = mapper;
            _cachingStrategyService = cachingStrategyService;
            _dummyUserRepository = dummyUserRepository;
            _pagingService = pagingService;
            _logger = logger;
        }

        public async Task<PaginatedView<UserView>?> GetPaginatedView(int page, int itemsPerPage = 5, CancellationToken cancellationToken = default)
        {
            try
            {
                var data = await _cachingStrategyService.ApplyAsync($"userList_{page}_{itemsPerPage}",
                (ct) => _dummyUserRepository.GetList(page, itemsPerPage, ct),
                cancellationToken);

                var result = _mapper.Map<PaginatedView<UserView>>(data);

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
