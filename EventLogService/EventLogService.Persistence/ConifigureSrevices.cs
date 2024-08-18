using EventLogService.Contacts.Persistence;
using EventLogService.Persistence.Context;
using EventLogService.Persistence.Settings;
using Foxera.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAccountService.Persistence.Context;

namespace EventLogService.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.GetGenericSettings<StorageSettings>(configuration);
        services.AddScoped<IEventsDbContext, EventsDbContext>();//we configure them and add them the services
        services.AddScoped<EventsDbContext>();//KERMEL EL HEALTH CHECK
        services.AddScoped<EventsDBInitialiser>();
        return services;
        
    }
}
