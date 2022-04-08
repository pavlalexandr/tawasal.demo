using Domain.Entities.DummyApi;
using Domain.Repository;
using Infrastructure.Refit.DummyApi;
namespace Infrastructure.Repository
{
    internal class UsersRepository : IDummyApiRepository<User>
    {
        private readonly IDummyApiRest _dummyApiRest;

        public UsersRepository(IDummyApiRest dummyApiRest)
        {
            _dummyApiRest = dummyApiRest;
        }

        public Task<Response<User>> GetList(int page, int limit, CancellationToken cancellationToken = default)
        {
            return _dummyApiRest.GetUsers(page, limit, cancellationToken);
        }

        public Task<Response<User>> GetListByParent(string parentId, int page, int limit, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
