using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Domain.Interfaces
{
    public interface IDataProviderStartup
    {
        IServiceCollection AddDataProviders(IServiceCollection services, IConfiguration configuration);
    }
}
