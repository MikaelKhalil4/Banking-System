

using Foxera.BackgroundJobs;
using Foxera.Common.Settings;
using Foxera.HealthCheck;
using Foxera.Keycloak;
using Foxera.Logging;
using Foxera.Mail;
using Foxera.RabitMq;
using Microsoft.Extensions.Options;
using UserAccountService.API;
using UserAccountService.API.StartUp;
using UserAccountService.Application;
using UserAccountService.Persistence;

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
app.UseHealthChecksService();
app.UseLoggingServices();
//


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();