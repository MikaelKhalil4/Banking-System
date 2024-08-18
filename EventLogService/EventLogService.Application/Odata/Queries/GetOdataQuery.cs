using EventLogService.Contacts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TransactionService.Application.Features.Odata.Queries;

public class GetOdataQuery : IRequest<IQueryable>
{
    public Type Type { get; set; }
}

public class GetOdataQueryHandler(IEventsDbContext context)
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