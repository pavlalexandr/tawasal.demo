using Application.Services;
using Application.Services.Caching;
using Application.Services.DateTimeService;
using Application.Services.Paging;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDIModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddScoped<ICachingStrategyService, CachingStrategyService>()
                .AddScoped<UsersService>()
                .AddScoped<UserPostsService>()
                .AddSingleton<IPagingService, PagingService>()
                .AddSingleton<IDateTimeService, DateTimeService>()
                .AddAutoMapper(typeof(ApplicationDIModule).Assembly);
        }
    }
}
