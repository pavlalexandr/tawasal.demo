using Application.Services.Paging;
using FluentAssertions;
using Xunit;

namespace Tests.Unit
{
    public class PagingServiceTest
    {
        [Theory]
        [InlineData(11,5)]
        [InlineData(15, 5)]
        [InlineData(14, 5)]
        public void GetTotalPages_GetCorrectPageCount(int itemsCount, int itemsPerPage)
        {
            var service = new PagingService();
            var result = service.GetTotalPages(itemsCount, itemsPerPage);
            result.Should().Be(3);
        }

        [Fact]
        public void GetTotalPages_WithoutRemaining_ReturnsAbsDevisionValue()
        {
            var service = new PagingService();
            var result = service.GetTotalPages(15, 5);
            result.Should().Be(3);
        }

        [Fact]
        public void GetTotalPages_WithRemaining_ReturnsIncrementedDevisionValue()
        {
            var service = new PagingService();
            var result = service.GetTotalPages(16, 5);
            result.Should().Be(4);
        }
    }
}
