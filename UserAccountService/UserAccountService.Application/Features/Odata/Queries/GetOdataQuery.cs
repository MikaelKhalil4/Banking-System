using Foxera.Keycloak.Contracts;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAccountService.Application.Helper;
using UserAccountService.Contracts.Persistence;
using UserAccountService.Domain.Entities;

namespace UserAccountService.Application.Features.Odata.Queries;

public class GetOdataQuery : IRequest<IQueryable>
{
    public Type Type { get; set; }
}

public class GetOdataQueryHandler(IAccountsDbContext context,ICurrentUser _currentUser)
    : IRequestHandler<GetOdataQuery, IQueryable>
{
    public async Task<IQueryable> Handle(GetOdataQuery request, CancellationToken cancellationToken)
    {
        var query= (IQueryable)context.GetType().GetMethods()
            .FirstOrDefault(x => x.Name == nameof(DbContext.Set) && x.GetParameters().Length == 0)?
            .MakeGenericMethod(request.Type)
            .Invoke(context, null)!;
        
        
        // Apply additional filtering for Accounts type
        if (request.Type == typeof(Account))
        {
            var predicate = PredicateBuilder.New<Account>( true);
            
            if (_currentUser.IsInRole("Customer"))//a customer can see only his accounts of all branches
            {
                predicate = predicate.And(x => x.UserId == _currentUser.Id);
            }

            query = ((IQueryable<Account>)query).Where(predicate);
        }

        return query;
    }
}