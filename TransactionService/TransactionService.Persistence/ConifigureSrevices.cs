using Foxera.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAccountService.Persistence.Settings;

namespace UserAccountService.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.GetGenericSettings<StorageSettings>(configuration);
        return services;
        
    }
}
