using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserAccountService.Persistence.Context;

public partial class AccountsDbContextInitialiser
{
    public readonly AccountsDbContext _context;
    public readonly ILogger<AccountsDbContextInitialiser> _logger;
    
    public AccountsDbContextInitialiser(AccountsDbContext context, ILogger<AccountsDbContextInitialiser> logger)
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