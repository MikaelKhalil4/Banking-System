using Foxera.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Contracts.Persistence;
using TransactionService.Domain.Entities;
using TransactionService.Persistence.Context;
using TransactionService.Persistence.Settings;


namespace TransactionService.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //NOTE enno kermel el migration tdaryna naht el el packages bel peristence!
        services.GetGenericSettings<StorageSettings>(configuration);
        services.AddScoped<ITransactionsDbContext, TransactionsDbContext>();//we configure them and add them the services
        services.AddScoped<TransactionsDbContext>();//KERMEL EL HEALTH CHECK
        services.AddScoped<TransactionsDbContextInitialiser>();
        return services;
        
    }
}
    