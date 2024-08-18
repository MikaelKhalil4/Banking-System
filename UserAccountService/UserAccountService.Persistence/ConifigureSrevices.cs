using Foxera.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAccountService.Contracts.Persistence;
using UserAccountService.Persistence.Context;
using UserAccountService.Persistence.Settings;

namespace UserAccountService.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.GetGenericSettings<StorageSettings>(configuration);
        services.AddScoped<IAccountsDbContext, AccountsDbContext>();//we configure them and add them the services
        services.AddScoped<AccountsDbContext>();//KERMEL EL HEALTH CHECK
        services.AddScoped<AccountsDbContextInitialiser>();
        return services;
    }
}