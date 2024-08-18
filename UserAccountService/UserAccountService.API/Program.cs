

using Foxera.Common.Settings;
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