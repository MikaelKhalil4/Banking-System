using System;
using System.Collections.Generic;
using EventLogService.Contacts.Persistence;
using EventLogService.Domain.Entities;
using EventLogService.Persistence.Settings;
using Microsoft.EntityFrameworkCore;

namespace EventLogService.Persistence.Context;

public partial class EventsDbContext : DbContext,IEventsDbContext
{
    private readonly StorageSettings _settings;
    
    public
        EventsDbContext(
            StorageSettings settings) //injection the settion that was initialized in the configure services
    {
        _settings = settings;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"{_settings.DefaultConnection}");
    }

    
//         public EventsDbContext()
//     {
//     }
//
//     public EventsDbContext(DbContextOptions<EventsDbContext> options)
//         : base(options)
//     {
//     }
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=localhost;Port=5432; Database=EventsDB;Username=postgres;Password=123");
//


    public virtual DbSet<Eventlog> Eventlogs { get; set; }

    public virtual DbSet<Eventtype> Eventtypes { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eventlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eventlogs_pkey");

            entity.ToTable("eventlog");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EventData)
                .HasColumnType("jsonb")
                .HasColumnName("event_data");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.EventType).WithMany(p => p.Eventlogs)
                .HasForeignKey(d => d.EventTypeId)
                .HasConstraintName("eventlogs_event_type_id_fkey");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Eventlogs)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("eventlogs_transactions_id_fkey");
        });

        modelBuilder.Entity<Eventtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eventtypes_pkey");

            entity.ToTable("eventtype");

            entity.HasIndex(e => e.TypeName, "eventtypes_type_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        });

        OnModelCreatingPartial(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
