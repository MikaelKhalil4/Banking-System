using System.Reflection;
using Microsoft.AspNetCore.OData;
using TransactionService.API.Odata;
using TransactionService.Persistence.Context;

namespace TransactionService.API;

public static class ConfigureServices
{
    public static void AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {

      
        
        var modelBuilder = new ODataModelBuilder();
        
        services.AddControllers().AddOData(
            options => options
                .Select()    // Enables clients to select specific properties to return
                .Filter()    // Allows clients to filter the results based on conditions
                .OrderBy()   // Permits clients to sort the results based on properties
                .Expand()    // Enables inclusion of related entities in the results
                .Count()     // Allows clients to request a count of the query results
                .SetMaxTop(null) // Removes any maximum limit on returned record count
                .SkipToken() // Added SkipToken for server-driven paging
                .AddRouteComponents("odata", modelBuilder.GetEdmModel()) // Sets up the OData endpoint routing
        );

        services.AddEndpointsApiExplorer();
        
        services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));
    }
}