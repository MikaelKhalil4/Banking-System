using EventLogService.API;
using EventLogService.API.Odata;
using EventLogService.API.StartUp;
using EventLogService.Application;
using EventLogService.Persistence;
using Foxera.Common.Settings;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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