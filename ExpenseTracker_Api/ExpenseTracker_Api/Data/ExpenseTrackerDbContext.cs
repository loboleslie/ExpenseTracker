namespace ExpenseTracker_Api.Data;

public class ExpenseTrackerDbContext : DbContext
{
	public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options) { }

	public DbSet<Account> Accounts { get; set; }
}

