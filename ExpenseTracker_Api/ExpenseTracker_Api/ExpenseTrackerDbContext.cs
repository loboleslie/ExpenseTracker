using ExpenseTracker_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExpenseTracker_Api.Data;

public class ExpenseTrackerDbContext : DbContext
{
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
}

