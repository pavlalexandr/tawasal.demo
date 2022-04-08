using Domain.Constants;
using Domain.Entities.DummyApi;
using Refit;

namespace Infrastructure.Refit.DummyApi
{
    public interface IDummyApiRest
    {
        [Get("/user")]
        Task<Response<User>> GetUsers(int page = Constants.DummyApi.DefaultPage, int limit = Constants.DummyApi.DefaultLimit, CancellationToken cancellationToken = default);

        [Get("/user/{userId}/post")]
        Task<Response<UserPost>> GetUserPosts(string userId, int page = Constants.DummyApi.DefaultPage, int limit = Constants.DummyApi.DefaultLimit, CancellationToken cancellationToken = default);

        [Get("/post")]
        Task<Response<UserPost>> GetUserPosts(int page = Constants.DummyApi.DefaultPage, int limit = Constants.DummyApi.DefaultLimit, CancellationToken cancellationToken = default);
    }
}
