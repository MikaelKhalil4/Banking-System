using EventLogService.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserAccountService.Persistence.Context;

public partial class EventsDBInitialiser
{
    public readonly EventsDbContext _context;
    public readonly ILogger<EventsDBInitialiser> _logger;
    
    public EventsDBInitialiser(EventsDbContext context, ILogger<EventsDBInitialiser> logger)
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