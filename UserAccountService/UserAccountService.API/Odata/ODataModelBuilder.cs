using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using UserAccountService.Domain.Entities;

namespace UserAccountService.API.Odata;

public class ODataModelBuilder
{
    public IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        //MAke sure Ykun el esem abel el controller huwwer zeito!!!
        builder.EntitySet<User>(nameof(User));
        builder.EntitySet<Account>(nameof(Account));
        builder.EntitySet<Branch>(nameof(Branch));
        builder.EntitySet<Location>(nameof(Location));
        builder.EntitySet<Role>(nameof(Role));

        return builder.GetEdmModel();
    }
}