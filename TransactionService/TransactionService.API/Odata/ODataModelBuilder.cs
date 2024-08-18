using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using TransactionService.Domain.Entities;

namespace TransactionService.API.Odata;

public class ODataModelBuilder
{
    public IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        //MAke sure Ykun el esem abel el controller huwwer zeito!!!
        builder.EntitySet<Currency>(nameof(Currency));
        builder.EntitySet<Account>(nameof(Account));
        builder.EntitySet<RecurrentTransaction>(nameof(RecurrentTransaction));
        builder.EntitySet<Transaction>(nameof(Transaction));
        builder.EntitySet<Transactiontype>(nameof(Transactiontype));

        return builder.GetEdmModel();
    }
}