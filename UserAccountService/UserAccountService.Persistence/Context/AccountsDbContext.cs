using Microsoft.EntityFrameworkCore;
using UserAccountService.Contracts.Persistence;
using UserAccountService.Domain.Entities;
using UserAccountService.Persistence.Settings;

namespace UserAccountService.Persistence.Context;

public partial class AccountsDbContext : DbContext, IAccountsDbContext
{
    private readonly StorageSettings _settings;

    public
        AccountsDbContext(
            StorageSettings settings) //injection the settion that was initialized in the configure services
    {
        _settings = settings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"{_settings.DefaultConnection}");
    }


    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("account");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasPrecision(15, 2)
                .HasColumnName("balance");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("accounts_branch_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("accounts_user_id_fkey");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("branches_pkey");

            entity.ToTable("branch");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Location).WithMany(p => p.Branches)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("branches_location_id_fkey");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("location");

            entity.HasIndex(e => e.LocationName, "locations_location_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.LocationName)
                .HasMaxLength(255)
                .HasColumnName("location_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            
            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
         
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Branch).WithMany(p => p.Users)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("users_branch_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsDbContext).Assembly);
        SeedData(modelBuilder);
    }

    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>().HasData(GetLocationScopeSeeds());
        modelBuilder.Entity<Branch>().HasData(GetBranchSeeds());
        modelBuilder.Entity<Role>().HasData(GetRoleSeeds());
    }

    private static List<Location> GetLocationScopeSeeds()
    {
        // Return a list of Location seed data
        return new List<Location>
        {
            new Location { Id = 1, LocationName = "Location 1" },
            new Location { Id = 2, LocationName = "Location 2" }
        };
    }

    private static List<Branch> GetBranchSeeds()
    {
        // Return a list of Branch seed data
        return new List<Branch>
        {
            new Branch { Id = 1, Name = "Branch 1", LocationId = 1 },
            new Branch { Id = 2, Name = "Branch 2", LocationId = 2 }
        };
    }

    private static List<Role> GetRoleSeeds()
    {
        // Return a list of Role seed data
        return new List<Role>
        {
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "Employee" },
            new Role { Id = 3, RoleName = "Customer" }
        };
    }
}