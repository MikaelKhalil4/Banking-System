using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TransactionService.Contracts.Persistence;
using TransactionService.Domain.Entities;
using TransactionService.Persistence.Settings;

namespace TransactionService.Persistence.Context;

public partial class TransactionsDbContext : DbContext, ITransactionsDbContext
{
    private readonly StorageSettings _settings;
    
    public TransactionsDbContext(StorageSettings settings) //injection the settion that was initialized in the configure services
    {
        _settings = settings;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"{_settings.DefaultConnection}");
    }
    
    

//     public TransactionsDbContext()
//     {
//     }
//
//     public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options)
//         : base(options)
//     {
//     }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=localhost;Port=5432; Database=TransactionsDB;Username=postgres;Password=123");


    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<RecurrentTransaction> RecurrentTransactions { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transactiontype> Transactiontypes { get; set; }
    public bool Test { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("currency_pkey");

            entity.ToTable("currency");

            entity.HasIndex(e => e.CurrencyCode, "currency_currency_code_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("currency_code");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .HasColumnName("currency_name");
        });

        modelBuilder.Entity<RecurrentTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recurrent_transactions_pkey");

            entity.ToTable("recurrent_transaction");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BgJobId).HasColumnName("bgJob_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.Transaction).WithMany(p => p.RecurrentTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("recurrent_transactions_Transactions_fk");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("transactions_account_id_fkey");

            entity.HasOne(d => d.Currency).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("transactions_currency_id_fkey");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("transactions_transaction_type_id_fkey");


        });

        modelBuilder.Entity<Transactiontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactiontypes_pkey");

            entity.ToTable("transactiontype");

            entity.HasIndex(e => e.TypeName, "transactiontypes_type_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .HasColumnName("type_name");
            
        });
        
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionsDbContext).Assembly);
        SeedData(modelBuilder);
    }

    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transactiontype>().HasData(GetLocationScopeSeeds());
        modelBuilder.Entity<Currency>().HasData(GetCurrencySeeds());
    }

    private static List<Transactiontype> GetLocationScopeSeeds()
    {
        // Return a list of Location seed data
        return new List<Transactiontype>
        {
            new Transactiontype { Id = 1, TypeName = "Deposit" },
            new Transactiontype { Id = 2, TypeName = "Withdrawal" }
        };  
    }

    private static List<Currency> GetCurrencySeeds()
    {
        return new List<Currency>
        {
            new Currency { Id = 1, CurrencyCode = "USD", CurrencyName = "US Dollar" },
            new Currency { Id = 2, CurrencyCode = "EUR", CurrencyName = "Euro" },
            new Currency { Id = 3, CurrencyCode = "LBP", CurrencyName = "Lebanese Pound" }
        };
    }
}