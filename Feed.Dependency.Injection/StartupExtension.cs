using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.DataProvider;
using Template.Domain.Interfaces;
using Template.Services;
using Template.Services.Interfaces;
using Twitter.Feed.Reader.Domain.Models;

namespace Template.Dependency.Injection
{
    public static class StartupExtension
    {
        public static void AddBindings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TwitterConfig>(config.GetSection("Twitter"));
            services.AddSingleton<IFeedService, FeedService>();
            services.AddSingleton<IDataProviderStartup, DataProviderStartup>();
            var serviceProvider = services.BuildServiceProvider();
            var providers = serviceProvider.GetServices<IDataProviderStartup>();
            foreach (var provider in providers)
            {
                provider.AddDataProviders(services, config);
            }
        }
    }
}
