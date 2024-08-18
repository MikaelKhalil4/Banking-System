using Microsoft.EntityFrameworkCore;
using EventLogService.Domain.Entities;

namespace EventLogService.Contacts.Persistence;

public interface IEventsDbContext
{
    public  DbSet<Eventlog> Eventlogs { get; set; }

    public  DbSet<Eventtype> Eventtypes { get; set; }

    public  DbSet<Transaction> Transactions { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}