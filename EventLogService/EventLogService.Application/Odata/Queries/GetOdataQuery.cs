using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionService.Contracts.Persistence;

namespace TransactionService.Application.Features.Odata.Queries;

public class GetOdataQuery : IRequest<IQueryable>
{
    public Type Type { get; set; }
}

public class GetOdataQueryHandler(ITransactionsDbContext context)
    : IRequestHandler<GetOdataQuery, IQueryable>
{
    public async Task<IQueryable> Handle(GetOdataQuery request, CancellationToken cancellationToken)
    {
        return (IQueryable)context.GetType().GetMethods()
            .FirstOrDefault(x => x.Name == nameof(DbContext.Set) && x.GetParameters().Length == 0)?
            .MakeGenericMethod(request.Type)
            .Invoke(context, null)!;
    }
}