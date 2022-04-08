using Application.Services;
using Application.Services.Caching;
using Blazored.LocalStorage;
using Domain.Models.Settings;
using Infrastructure.Refit.DummyApi;
using Infrastructure.Services.Authentification;
using Infrastructure.Services.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
namespace Infrastructure
{
    public static class InfrastructureDIModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ApplicationSettingsModel();

            configuration.GetRequiredSection("ApplicationSettings").Bind(settings);

            services.AddSingleton(settings)
                    .AddSingleton<IAuthentificationService, AuthentificationService>()
                    .AddBlazoredLocalStorage()
                    .AddScoped<ITemporaryClientCachingService, TemporaryClientCachingService>()
                    .AddRepositories()
                    .AddRefitClient<IDummyApiRest>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(settings.DummyApiSettings.BaseUri);
                        c.DefaultRequestHeaders.Add(settings.DummyApiSettings.ApiKeyHeader, settings.DummyApiSettings.ApiKey);
                    });

            return services;
        }
    }
}
