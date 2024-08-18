using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NHibernate.Transaction;
using Serilog;
using TransactionService.Contracts.Persistence;

namespace TransactionService.Persistence.Context;

public partial class TransactionsDbContextInitialiser
{
    public readonly TransactionsDbContext _context;
    public readonly ILogger<TransactionsDbContextInitialiser> _logger;
    
    public TransactionsDbContextInitialiser(TransactionsDbContext context, ILogger<TransactionsDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public void Initialize()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                var pendingMigrations = _context.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    _context.Database.Migrate(); //it will update our db
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}