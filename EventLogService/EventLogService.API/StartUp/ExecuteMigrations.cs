using Microsoft.EntityFrameworkCore.Migrations.Internal;
using UserAccountService.Persistence.Context;

namespace UserAccountService.API.StartUp;

public static class ExecuteMigrations
{
    public static IApplicationBuilder ExecuteDbMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Migrator>>();
        var initializer = scope.ServiceProvider.GetRequiredService<AccountsDbContextInitialiser>();

        logger.LogInformation("Starting DB migration");

         initializer.Initialize();

        logger.LogInformation("DB migration complete");

        return app;
    }
}