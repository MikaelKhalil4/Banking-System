﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TransactionService.Persistence.Context;

#nullable disable

namespace TransactionService.Persistence.Migrations
{
    [DbContext(typeof(TransactionsDbContext))]
    [Migration("20240818183251_long ot string")]
    partial class longotstring
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TransactionService.Domain.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint")
                        .HasColumnName("branch_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("accounts_pkey");

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("currency_code");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("currency_name");

                    b.HasKey("Id")
                        .HasName("currency_pkey");

                    b.HasIndex(new[] { "CurrencyCode" }, "currency_currency_code_key")
                        .IsUnique();

                    b.ToTable("currency", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrencyCode = "USD",
                            CurrencyName = "US Dollar"
                        },
                        new
                        {
                            Id = 2,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                        new
                        {
                            Id = 3,
                            CurrencyCode = "LBP",
                            CurrencyName = "Lebanese Pound"
                        });
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.RecurrentTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<string>("BgJobId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bgJob_id");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint")
                        .HasColumnName("transaction_id");

                    b.HasKey("Id")
                        .HasName("recurrent_transactions_pkey");

                    b.HasIndex("TransactionId");

                    b.ToTable("recurrent_transaction", (string)null);
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Amount")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric(15,2)")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("transaction_type_id");

                    b.HasKey("Id")
                        .HasName("transactions_pkey");

                    b.HasIndex("AccountId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("transaction", (string)null);
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Transactiontype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("type_name");

                    b.HasKey("Id")
                        .HasName("transactiontypes_pkey");

                    b.HasIndex(new[] { "TypeName" }, "transactiontypes_type_name_key")
                        .IsUnique();

                    b.ToTable("transactiontype", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Deposit"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Withdrawal"
                        });
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.RecurrentTransaction", b =>
                {
                    b.HasOne("TransactionService.Domain.Entities.Transaction", "Transaction")
                        .WithMany("RecurrentTransactions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("recurrent_transactions_Transactions_fk");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("TransactionService.Domain.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_account_id_fkey");

                    b.HasOne("TransactionService.Domain.Entities.Currency", "Currency")
                        .WithMany("Transactions")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_currency_id_fkey");

                    b.HasOne("TransactionService.Domain.Entities.Transactiontype", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_transaction_type_id_fkey");

                    b.Navigation("Account");

                    b.Navigation("Currency");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Currency", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Transaction", b =>
                {
                    b.Navigation("RecurrentTransactions");
                });

            modelBuilder.Entity("TransactionService.Domain.Entities.Transactiontype", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
