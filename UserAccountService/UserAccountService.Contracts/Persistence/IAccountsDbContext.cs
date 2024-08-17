using Microsoft.EntityFrameworkCore;
using UserAccountService.Domain.Entities;

namespace UserAccountService.Contracts.Persistence;

public interface IAccountsDbContext
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Branch> Branches { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}