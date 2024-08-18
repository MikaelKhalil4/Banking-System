using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAccountService.Contracts.Persistence;
using UserAccountService.Domain.Entities;

namespace UserAccountService.Application.Features.Accounts.Command;

public class CreateAccountCommand : IRequest<CreateAccountResponse>
{
    public int UserId { get; set; }
    public decimal Balance { get; set; }
}

#region Response

public class CreateAccountResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    
    public Account? NewAccount { get; set; }
}


#endregion

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
{
    private readonly IAccountsDbContext _context;

    public CreateAccountCommandHandler(IAccountsDbContext context)
    {
        _context = context;
    }


    public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountCount = await _context.Accounts
            .Where(a => a.UserId == request.UserId && !a.IsDeleted)
            .CountAsync(cancellationToken);

        if (accountCount >= 5)
        {
            return new CreateAccountResponse
            {
                Success = false,
                Message = "The customer already has the maximum number of accounts."
            };
        }
        
        var newAccount = new Account
        {
            UserId = request.UserId,
            Balance = request.Balance,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            BranchId = 1,
            IsDeleted = false
        };

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateAccountResponse
        {
            Success = true,
            Message = "Account created successfully.",
            NewAccount = newAccount
        };
    }
}