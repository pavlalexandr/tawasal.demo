using Domain.Entities.DummyApi;
using Domain.Repository;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    internal static class RepositoriesModule
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IDummyApiRepository<UserPost>, UserPostsRepository>()
                .AddScoped<IDummyApiRepository<User>, UsersRepository>();
        }
    }
}