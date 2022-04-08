namespace Domain.Models.Caching
{
    public class CachedValue<T>
    {
        public T Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
