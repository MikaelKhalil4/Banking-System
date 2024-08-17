using EventLogService.Domain.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace EventLogService.API.Odata;

public class ODataModelBuilder
{
    public IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        //MAke sure Ykun el esem abel el controller huwwer zeito!!!
        builder.EntitySet<Eventlog>(nameof(Eventlog));
        builder.EntitySet<Eventtype>(nameof(Eventtype));
        return builder.GetEdmModel();
    }
}