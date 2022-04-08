namespace Domain.Entities.DummyApi
{
    public class Response<T>  where T : BaseEntity
    {
        public IReadOnlyList<T> Data { get; set; } = new List<T>();
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
