namespace Application.Services.Paging
{
    public class PagingService : IPagingService
    {
        public int GetTotalPages(int itemsCount, int itemsPerPage)
        {
            var basePages = Math.Abs(itemsCount / itemsPerPage);
            return (itemsCount - itemsPerPage * basePages) > 0 ? ++basePages : basePages;
        }
    }
}
