using Foxera.Keycloak.Contracts;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionService.Contracts.Persistence;
using TransactionService.Domain.Entities;


namespace TransactionService.Application.Features.Odata.Queries;

public class GetOdataQuery : IRequest<IQueryable>
{
    public Type Type { get; set; }
}

public class GetOdataQueryHandler(ITransactionsDbContext context, ICurrentUser _currentUser)
    : IRequestHandler<GetOdataQuery, IQueryable>
{
    
    public async Task<IQueryable> Handle(GetOdataQuery request, CancellationToken cancellationToken)
    {
        var query = (IQueryable)context.GetType().GetMethods()
            .FirstOrDefault(x => x.Name == nameof(DbContext.Set) && x.GetParameters().Length == 0)?
            .MakeGenericMethod(request.Type)
            .Invoke(context, null)!;
        
        // Apply additional filtering for Transactions type
        if (request.Type == typeof(Transaction))
        {
            var predicate = PredicateBuilder.New<Transaction>( true);
            
            if (_currentUser.IsInRole("Customer"))//a customer can see only his tranactions of al of his accounts
            {
                predicate = predicate.And(x => x.Account.UserId == _currentUser.Id);
            }

            query = ((IQueryable<Transaction>)query).Where(predicate);
        }

        return query;
    }
}