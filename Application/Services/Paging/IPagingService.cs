namespace Application.Services.Paging
{
    public interface IPagingService
    {
        int GetTotalPages(int itemsCount, int itemsPerPage);
    }
}