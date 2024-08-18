using Microsoft.EntityFrameworkCore.Migrations.Internal;
using UserAccountService.Persistence.Context;

namespace EventLogService.API.StartUp;

public static class ExecuteMigrations
{
    public static IApplicationBuilder ExecuteDbMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Migrator>>();
        var initializer = scope.ServiceProvider.GetRequiredService<EventsDBInitialiser>();

        logger.LogInformation("Starting DB migration");

         initializer.Initialize();

        logger.LogInformation("DB migration complete");

        return app;
    }
}