using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TransactionService.Domain.Entities;

namespace TransactionService.Contracts.Persistence;

public interface ITransactionsDbContext
{
    public  DbSet<Account> Accounts { get; set; }

    public  DbSet<Currency> Currencies { get; set; }

    public  DbSet<RecurrentTransaction> RecurrentTransactions { get; set; }

    public  DbSet<Transaction> Transactions { get; set; }

    public  DbSet<Transactiontype> Transactiontypes { get; set; }
    
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}