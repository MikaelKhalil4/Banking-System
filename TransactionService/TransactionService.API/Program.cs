using Foxera.BackgroundJobs;
using Foxera.Common.Settings;
using Foxera.HealthCheck;
using Foxera.Keycloak;
using Foxera.Logging;
using Foxera.Logging.Middlewares;
using Foxera.Mail;
using TransactionService.API;
using TransactionService.API.StartUp;
using TransactionService.Persistence;
using UserAccountService.Application;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
// Add services to the container.

services.AddControllers();
services.AddSwaggerGen();

services.AddApplicationServices(configuration);
services.AddPersistenceServices(configuration);
services.AddWebApiServices(configuration);//ejbare tahet el persistence
services.AddAuthenticationServices(configuration);
services.AddTransient<IStartupFilter, SettingValidationStartupFilter>();//kermel naamil the validation for all settings
services.AddSwaggerServices();


//custom
services.AddBackgroundJobsSettings(configuration);
services.AddBackgroundJobServices();
services.AddMailServices(configuration);
services.AddHealthCheckServices(configuration);
builder.AddLoggingServices();
//
var app = builder.Build();

app.ExecuteDbMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerServices();
}

//custom
app.UseBackgroundJobServices();
app.UseHealthChecksService();
app.UseLoggingServices();
//
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();