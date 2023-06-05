using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BatuBankClassLibary.Entities;

public partial class BatuBankContext : DbContext
{
    public BatuBankContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Important.
    }

    public BatuBankContext(DbContextOptions<BatuBankContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<UserProduct> UserProducts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<CreditCardType> CreditCardTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserLoginObserver> UserLoginObservers { get; set; }

    public virtual DbSet<UserAccountObserver> UserAccountObservers { get; set; }

    public virtual DbSet<UserCreditCard> UserCreditCards { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserTransaction> UserTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=BatuBank;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Iban).HasColumnName("IBAN");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccountType_pkey");

            entity.ToTable("AccountType");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CreditCard_pkey");

            entity.ToTable("CreditCard");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<CreditCardType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CreditCardType_pkey");

            entity.ToTable("CreditCardType");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pkey");

            entity.ToTable("Product");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TransactionType_pkey");

            entity.ToTable("TransactionType");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserAccount_pkey");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserAccountObserver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserAccountObserver_pkey");

            entity.ToTable("UserAccountObserver");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserCreditCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserCreditCard_pkey");

            entity.ToTable("UserCreditCard");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey");

            entity.ToTable("UserRole");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserTransaction_pkey");

            entity.ToTable("UserTransaction");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserLoginObserver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserLoginObserver_pkey");

            entity.ToTable("UserLoginObserver");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserProduct_pkey");

            entity.ToTable("UserProduct");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
