using ExpenseTracker_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExpenseTracker_Api.Data;

public class ExpenseTrackerDbContext : DbContext
{
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
    
    public DbSet<AccountPayee> AccountPayees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountPayee>()
            .HasKey(ap => new { ap.AccountId, ap.PayeeId });
    }

}

