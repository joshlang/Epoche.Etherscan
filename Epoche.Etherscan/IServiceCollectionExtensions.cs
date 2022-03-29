using Microsoft.Extensions.DependencyInjection;

namespace Epoche.Etherscan;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEtherscanClient(this IServiceCollection services, Action<EtherscanClientOptions>? configure = null)
    {
        services.AddSingleton<EtherscanClient>();
        if (configure is not null)
        {
            services.Configure(configure);
        }
        return services;
    }
}
