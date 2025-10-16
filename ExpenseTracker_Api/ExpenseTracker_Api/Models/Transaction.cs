using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker_Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public required int AccountId { get; set;}
        
        public DateTimeOffset TransactionDate { get; set; }

        public required Account Account { get; init; }
        public decimal Amount { get; set; }
    }
}
