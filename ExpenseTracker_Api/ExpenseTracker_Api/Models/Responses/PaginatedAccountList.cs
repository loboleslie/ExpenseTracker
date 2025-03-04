namespace ExpenseTracker_Api.Models.Responses;

public class PaginatedAccountList
{
    public List<Account>? Accounts { get; set; }
    public int TotalCount { get; set; }
}