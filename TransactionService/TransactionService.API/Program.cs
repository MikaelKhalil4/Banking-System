using Foxera.Common.Settings;
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
services.AddWebApiServices(configuration);

services.AddTransient<IStartupFilter, SettingValidationStartupFilter>();//kermel naamil the validation for all settings

var app = builder.Build();

app.ExecuteDbMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();