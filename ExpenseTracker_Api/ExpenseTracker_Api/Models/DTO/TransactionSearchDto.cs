namespace ExpenseTracker_Api.Models.DTO;

public class TransactionSearchDto
{
    public DateTimeOffset? FromDate { get; set; } = DateTimeOffset.UtcNow.AddDays(-1);
    public DateTimeOffset? ToDate { get; set; } = DateTimeOffset.UtcNow;
    public int AccountId {  get; set; }
    public string SearchTerm { get; set; } = "";
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set;  } = 1;
}