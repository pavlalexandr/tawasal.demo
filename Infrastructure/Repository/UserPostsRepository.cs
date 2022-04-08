using Domain.Entities.DummyApi;
using Domain.Repository;
using Infrastructure.Refit.DummyApi;

namespace Infrastructure.Repository
{
    internal class UserPostsRepository : IDummyApiRepository<UserPost>
    {
        private readonly IDummyApiRest _dummyApiRest;

        public UserPostsRepository(IDummyApiRest dummyApiRest)
        {
            _dummyApiRest = dummyApiRest;
        }

        public Task<Response<UserPost>> GetList(int page, int limit, CancellationToken cancellationToken = default)
        {
            return _dummyApiRest.GetUserPosts(page, limit, cancellationToken);
        }

        public Task<Response<UserPost>> GetListByParent(string parentId, int page, int limit, CancellationToken cancellationToken = default)
        {
            return _dummyApiRest.GetUserPosts(parentId, page, limit, cancellationToken);
        }
    }
}
