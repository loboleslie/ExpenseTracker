namespace ExpenseTracker_Api.Models.DTO;

public class TransactionDto
{
    public string? Description { get; set; }

    public int AccountId { get; set;}  
        
    public DateTimeOffset TransactionDate { get; set; }
    public decimal Amount { get; set; }
}