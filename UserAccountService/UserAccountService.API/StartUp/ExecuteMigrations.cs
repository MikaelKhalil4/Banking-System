using Microsoft.EntityFrameworkCore.Migrations.Internal;
using TransactionService.Persistence.Context;

namespace TransactionService.API.StartUp;

public static class ExecuteMigrations
{
    public static IApplicationBuilder ExecuteDbMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Migrator>>();
        var initializer = scope.ServiceProvider.GetRequiredService<TransactionsDbContextInitialiser>();

        logger.LogInformation("Starting DB migration");

         initializer.Initialize();

        logger.LogInformation("DB migration complete");

        return app;
    }
}