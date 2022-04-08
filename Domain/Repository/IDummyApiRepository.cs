using Domain.Entities.DummyApi;

namespace Domain.Repository
{
    public interface IDummyApiRepository<T> where T: BaseEntity
    {
        Task<Response<T>> GetList(int page, int limit, CancellationToken cancellationToken = default(CancellationToken));
        Task<Response<T>> GetListByParent(string parentId, int page, int limit, CancellationToken cancellationToken = default(CancellationToken));
    }
}
